// Infrastructure for personal-portfolio on Azure Container Apps
// Provisions:
// - Log Analytics workspace (for Container Apps logs)
// - Container Apps Environment
// - Two Container Apps (API & Frontend) with system-assigned managed identity
// - ACR pull role assignments (assumes ACR already exists; created by workflow)
// NOTE: ACR admin user remains disabled. Images passed as parameters.

@description('Deployment location')
param location string

@description('Existing ACR name (created separately)')
param acrName string

@description('Container Apps Environment name')
param envName string

@description('API Container App name')
param apiAppName string

@description('Frontend Container App name')
param frontendAppName string

@description('Full image reference for API (registry/repo:tag)')
param apiImage string

@description('Full image reference for Frontend (registry/repo:tag)')
param frontendImage string

@description('Minimum replicas (0 enables scale to zero)')
@minValue(0)
@maxValue(5)
param minReplicas int = 0

@description('Maximum replicas for autoscaling')
@minValue(1)
@maxValue(30)
param maxReplicas int = 5

var logWorkspaceName = '${envName}-logs'
var logWorkspaceSku = 'PerGB2018'

// Existing ACR reference
resource acr 'Microsoft.ContainerRegistry/registries@2023-07-01' existing = {
  name: acrName
}

resource logws 'Microsoft.OperationalInsights/workspaces@2023-09-01' = {
  name: logWorkspaceName
  location: location
  properties: {
    retentionInDays: 30
    features: {
      searchVersion: 2
    }
  }
  sku: {
    name: logWorkspaceSku
  }
}

resource caEnv 'Microsoft.App/managedEnvironments@2024-03-01' = {
  name: envName
  location: location
  properties: {
    appLogsConfiguration: {
      destination: 'log-analytics'
      logAnalyticsConfiguration: {
        customerId: logws.properties.customerId
        sharedKey: listKeys(logws.id, logws.apiVersion).primarySharedKey
      }
    }
  }
}

@description('API Container App')
resource apiApp 'Microsoft.App/containerApps@2024-03-01' = {
  name: apiAppName
  location: location
  identity: {
    type: 'SystemAssigned'
  }
  properties: {
    managedEnvironmentId: caEnv.id
    configuration: {
      activeRevisionsMode: 'Single'
      ingress: {
        external: true
        targetPort: 8080
        transport: 'auto'
      }
      registries: [
        {
          server: acr.properties.loginServer
          identity: 'system'
        }
      ]
    }
    template: {
      containers: [
        {
          name: 'api'
          image: apiImage
          probes: [
            {
              type: 'Liveness'
              httpGet: {
                path: '/api/status'
                port: 8080
              }
              initialDelaySeconds: 10
              periodSeconds: 30
            }
          ]
        }
      ]
      scale: {
        minReplicas: minReplicas
        maxReplicas: maxReplicas
        rules: [
          {
            name: 'http-concurrency'
            http: {
              metadata: {
                concurrentRequests: '50'
              }
            }
          }
        ]
      }
    }
  }
}

@description('Frontend Container App')
resource frontendApp 'Microsoft.App/containerApps@2024-03-01' = {
  name: frontendAppName
  location: location
  identity: {
    type: 'SystemAssigned'
  }
  properties: {
    managedEnvironmentId: caEnv.id
    configuration: {
      activeRevisionsMode: 'Single'
      ingress: {
        external: true
        targetPort: 80
        transport: 'auto'
      }
      registries: [
        {
          server: acr.properties.loginServer
          identity: 'system'
        }
      ]
    }
    template: {
      containers: [
        {
          name: 'frontend'
          image: frontendImage
        }
      ]
      scale: {
        minReplicas: minReplicas
        maxReplicas: maxReplicas
      }
    }
  }
}

// Role assignments for ACR pull (system-assigned identities)
// NOTE: Role assignments for AcrPull removed to avoid requiring elevated 'User Access Administrator'.
// Ensure container app managed identities have ACR pull rights externally (az role assignment create ...).

output apiFqdn string = apiApp.properties.configuration.ingress.fqdn
output frontendFqdn string = frontendApp.properties.configuration.ingress.fqdn
