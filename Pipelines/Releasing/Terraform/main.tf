terraform {
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "~> 2.65"
    }
  }
  # This is needed for local commands like creating the workspaces
  backend "azurerm" {
    resource_group_name  = "matthias"
    storage_account_name = "terraformstuff"
    container_name       = "terraformstuffblob"
    key                  = "tf/terraform.tfstate"
  }
}

provider "azurerm" {
  subscription_id = var.subscription_id
  tenant_id       = "d6fddda6-f690-4755-92c2-f22a3521bab0"
  features {
  }
}

resource "azurerm_app_service_plan" "res" {
  name                = lower(substr(join("", ["$(EnvPrefix)", var.app_name]), 0, 24))
  location            = var.region
  resource_group_name = var.resource_group_name
  kind                = "FunctionApp"

  sku {
    tier = "Dynamic"
    size = "Y1"
  }
}

resource "azurerm_storage_account" "res" {
  name                     = lower(substr(join("", ["$(EnvPrefix)", var.app_name]), 0, 24))
  resource_group_name      = var.resource_group_name
  location                 = var.region
  account_tier             = "Standard"
  account_replication_type = "LRS"
}

resource "azurerm_function_app" "res" {
  name                       = var.app_name
  location                   = var.region
  resource_group_name        = var.resource_group_name
  app_service_plan_id        = azurerm_app_service_plan.res.id
  storage_account_name       = azurerm_storage_account.res.name
  storage_account_access_key = azurerm_storage_account.res.primary_access_key
  site_config {
    dotnet_framework_version = "v4.0"
  }
  version = "~3"
}
