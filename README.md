# Mini Loan Application
🌐 **[Live API Swagger UI](https://mini-loan-app-api-bdbzafd7a6dkb2dm.australiaeast-01.azurewebsites.net/swagger/index.html)**

## 🎯 Overview
This is a **cloud-ready .NET 8 production-minded app 💼** that simulates a minimal loan application system.  

Features:
- 📑 Loan application & auto-approval business logic
- 🔄 Loan query & repayment schedule generation (EMI formula)
- 📡 Event-driven design: API → Azure Service Bus → Azure Function consumer
- 🔒 Security-first: API Key validation + centralized exception handling middleware
- 🧾 Compliance-ready: Audit logging middleware for traceability
- ☁️ Cloud-ready: Azure SQL, Key Vault, Service Bus, Functions
- 🤖 DevOps: CI/CD with GitHub Actions (build → test → deploy)
- 📊 Unit-tested business rules (loan approvals, EMI calculations)
- 📈 Extensible with 3rd-party services (Email, SMS)


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
### 3. Cross-Cutting Concerns (Middleware)

- 🔑 API Key Middleware → Secures APIs from unauthorized access
- 🛡️ Exception Handling Middleware → Catches and logs unhandled errors, returns safe responses
- 🧾 Request Logging Middleware → Provides an audit trail for loan applications & approvals

### 4. Cloud-Ready Infrastructure
- **Database**: runs locally on PostgreSQL, but easily switchable to **Azure SQL** with EF Core provider change
- **Secrets**: prepared for **Azure Key Vault** integration (DB connection strings, Service Bus keys)
- **Deployment**: runs locally with Docker, deployable to **Azure App Service**

## ✅ CI/CD & Automated Testing

This project includes a GitHub Actions pipeline that:

- Builds the solution on every push to `master`
- Runs all unit tests (`xUnit`) automatically
- Publishes to Azure App Service if tests pass

CI/CD ensures every merged feature is tested before going live.

## 🔒 Secrets Management
For this demo:
- API Key is stored in appsettings.Development.json(easy for local test)
- Database connection string uses local PostgreSQL(empty, replace your password)
- Azure Service Bus key is also kept in local configuration(empty, replace your key)

For production:
- All secrets (**API keys, DB connection strings, Service Bus keys**) should be stored in Azure Key Vault




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
- ✅ Step 8: Deploy API to Azure App Service (Swagger available online)
- ✅ Step 9: Deploy Azure Function to Azure (auto triggered by Service Bus events)
- 🔜 Step 10: Security & Compliance Enhancements
  - Logging/Audit Middleware (in progress)
  - Health checks & observability (planned)
