# Mini Loan Application

## 🎯 Overview
This is a **cloud-ready .NET 8 demo project** that simulates a minimal loan application system.  

Features:
- Loan application & auto-approval business logic
- Data persistence with EF Core + relational database
- Event-driven design with Azure Service Bus & Functions
- Deployment readiness with CI/CD, Docker, and App Service
- Secure configuration with Azure Key Vault (extensible)

---

## 📚 What This Project Can Do

### 1. Loan Management
- **Apply for a loan**
    - Input: applicant name, principal, annual interest rate, term months, optional score
    - Automatic approval rules:
        - Score < 0.5 → **Rejected**
        - Principal ≤ 50,000 and Rate ≤ 20% and Term ≤ 84 → **Approved**
        - Otherwise → **Pending**
- **Query a loan** by Id
- **Generate repayment schedule** using **equal installment (EMI)** formula

### 2. Event-Driven Notification
- When a loan is **Approved**, an event is published to **Azure Service Bus**
- An **Azure Function** subscribes to these events and processes them
    - In demo: logs the event
    - In extension: can send Email (SendGrid) or SMS (Twilio)

### 3. Cloud-Ready Infrastructure
- **Database**: runs locally on PostgreSQL, but easily switchable to **Azure SQL** with EF Core provider change
- **Secrets**: prepared for **Azure Key Vault** integration (DB connection strings, Service Bus keys)
- **Deployment**: runs locally with Docker, deployable to **Azure App Service**


## 🛠️ Tech Stack
- **Framework**: ASP.NET Core 8
- **ORM**: EF Core 8
- **Database**: PostgreSQL (local), Azure SQL (cloud option)
- **Cloud Services**:
    - Azure App Service
    - Azure Service Bus
    - Azure Functions
    - Azure Key Vault
    -  Azurite (local Azure Storage emulator for testing)
- **DevOps**: GitHub Actions, Docker
- **Testing**: xUnit, FluentAssertions

---

## 🚀 Roadmap
- ✅ Step 1: Database connection + Init migration (PostgreSQL, local dev)
- ✅ Step 2: Loan Application flow (create loan, query loan by Id)
- ✅ Step 3: Repayment schedule generation(Equal Monthly Installment -EMI)
- ✅ Step 4: Unit tests for core loan logic (xUnit + FluentAssertions)
- ✅ Step 5: Event publishing with Azure Service Bus (Topic based)
- ✅ Step 6: Azure Function consumer (listens to Service Bus, simulates loan notifications)
- ✅ Step 7: CI/CD pipeline with GitHub Actions (build, test, deploy)
- 🔜 Step 8: Deploy API to Azure App Service (Swagger available online)
- ✅ Step 9: Deploy Azure Function to Azure (auto triggered by Service Bus events)

