variable "region" {
  type    = string
  default = "West Europe"
}

variable "subscription_id" {
  type    = string
  default = "91660754-3529-407f-8458-92759935fbf7"
}

variable "app_name" {
  type    = string
  default = "$(AppName)"
}

variable "resource_group_name" {
  type    = string
  default = "matthias"
}
