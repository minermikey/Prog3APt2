# 🌱 Agri Energy Connect

Agri Energy is a user-friendly web application displaying green energy solutions for the agriculture sector. It allows customers to shop directly for fresh produce from farmers.

---

## 📖 Table of Contents

* [Project Description](#📌-project-description)
* [Features](#✨-features)
* [Installation](#🛠-installation)
* [Usage](#🚀-usage)
* [Technologies Used](#🧰-technologies-used)

---

## 📌 Project Description

**🎯 Motivation:**
With the rise in demand for organic and locally sourced produce, we wanted to build a platform that bridges the gap between farmers and consumers. Aside from wanting to make a better community, a key feature in creating this application was the possibility for future employment and convincing the employer that I am competent in C#.

**🧩 Problem It Solves:**
Some farmers lack a digital presence. Agri Energy offers them a space to showcase and sell their produce. It unifies a platform for small and large farmers to display their products in one location. The application also connects individuals through discussion forums.

**🧠 What I Learned:**
While working on this project, I enhanced and regained my knowledge of C# and MVC architecture. I also improved my front-end styling skills with CSS.

**🌟 What Makes It Stand Out:**

* Big, clean visual images using full-screen [hero](https://vwo.com/glossary/hero-image/#:~:text=What%20is%20a%20hero%20image,directly%20beneath%20the%20website%20header) design.
* SQL Server was used for storage integration for data management.
* Easy-to-navigate layout with a shopping UI.

---

## ✨ Features

### 🔐 Authentication & Authorization

* **Role-Based Access Control** with 3 distinct roles:

  * **Admin** – Full control to manage users, farmers, shop content, and approvals.
  * **Farmer** – Can upload products, manage listings, view stats, and message users.
  * **User** – Can browse and shop agricultural products and message sellers.

* Secure login, registration, and role assignment using ASP.NET Identity.

---

### 🛒 Shop Front

* The public-facing storefront is **accessible without login**.

* Showcases products from verified and approved farmers.

* Each product displays:

  * 📷 Product images
  * 📝 Descriptions
  * 💵 Pricing
---

### ✅ Farmer Approval Workflow

* **New Farmer Registration** starts with "Pending" status.
* Admins/Employees can:

  * ✔️ Approve
  * ❌ Reject
* Only approved farmers can access the dashboard and list products.

---

### 💬 Messaging System

* Built-in messaging between:

  * 👩‍🌾 Farmers ↔ Users
  * 🛡️ Admins ↔ Farmers
* Organized threads per user with timestamps.
* Farmers can answer product inquiries, and admins can send platform-related updates or actions.

---

### 📦 Product Management (Farmer Dashboard)

* Farmers can:

  * Add new product listings
  * Edit existing listings
  * Upload images and descriptions
  * Update stock availability

* Instant reflection of updates in the storefront.

---

### ☁️ Azure Cloud Integration

* **Azure Table Storage** for managing:

  * Farmer data
  * Product entries
  * User information

* **Azure File Storage** for:

  * Product images
  * Document uploads for admin verification

---

### 📊 Admin Dashboard

* View key platform metrics: 👥 Users, 👨‍🌾 Farmers, 📦 Products
* Approve or reject farmer requests
* Manage product listings
* Moderate flagged content/messages

---

### 📧 Notifications (Planned)

* Email-based alerts for:

  * Farmer approval results
  * New messages
  * Product activity

* Future integration with **SendGrid** or **MailKit** planned.

---

### 🧑‍🤝‍🧑 Community Discussion Forum

* Registered users can:

  * Post questions or experiences
  * Start discussions on sustainable farming
  * Get tips and feedback from farmers and buyers

---

### 🌐 Accessibility & UI Design

* Clean and minimal UI
* High contrast mode support for better visibility
* Optimized for slower internet connections
* Touch-friendly layout

---

## 🛠 Installation

1. Clone the repository:

   ```bash
   git clone https://github.com/minermikey/Prog3APt2.git
   ```

2. Navigate to the project directory:

   ```bash
   cd Prog3APt2
   ```

3. Open the project in Visual Studio or VS Code.

4. Make sure you have the following installed:

   * [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download)

5. Scaffold the database context:

   ```bash
   dotnet ef dbcontext scaffold "Server=Your-Server-Name;Database=Your-Database;Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o Models --force
   ```

6. Run the application:

   ```bash
   dotnet run
   ```

---

## 🚀 Usage

Once the application is running:

* View the home page featuring a hero image with "Agri Energy" branding.
* Scroll to the **Visit Our Farm Shop** section.
* Click **Go to Shop** to start browsing products.
* Users can register to:

  * Leave product reviews
  * Access the discussion forum
* Farmers can log in to manage and list their products.

---

## 🧰 Technologies Used

* **ASP.NET Core MVC**
* **Entity Framework Core**
* **SQL Server**
* **HTML & CSS**
* **Visual Studio 2022 / Visual Studio Code**
