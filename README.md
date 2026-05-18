# 🍔 Foodyy — Online Food Ordering System

A full-stack **ASP.NET Core MVC** web application for online food ordering, built with a clean **3-Tier Architecture** using Entity Framework Core, AutoMapper, and SQL Server.

---

## 📋 Table of Contents

- [Overview](#overview)
- [Architecture](#architecture)
- [Features](#features)
- [Tech Stack](#tech-stack)
- [Database Schema](#database-schema)
- [Project Structure](#project-structure)
- [Getting Started](#getting-started)
- [Default Credentials](#default-credentials)
- [Functionalities](#functionalities)
- [API Endpoints](#api-endpoints)
- [Screenshots](#screenshots)

---

## 🌟 Overview

**Foodyy** is an online food ordering platform where customers can browse food items, add them to a cart, place orders, and track their delivery status. Admins can manage users, categories, food items, and orders from a dedicated dashboard.

---

## 🏗️ Architecture

```
┌─────────────────────────────────────┐
│         APP (Presentation Layer)    │
│  Controllers + Views + Session      │
└────────────────┬────────────────────┘
                 │
┌────────────────▼────────────────────┐
│       BAL (Business Logic Layer)    │
│  Services + DTOs + AutoMapper       │
└────────────────┬────────────────────┘
                 │
┌────────────────▼────────────────────┐
│      DAL (Data Access Layer)        │
│  Repos + EF Core + SQL Server       │
└─────────────────────────────────────┘
```

### Layer Responsibilities

| Layer | Responsibility |
|-------|---------------|
| **APP** | Handles HTTP requests, sessions, views, user interface |
| **BAL** | Business logic, validation, AutoMapper DTO conversion |
| **DAL** | Database operations, Entity Framework, SQL Server |

---

## ✅ Features

### Authentication
- ✅ User Registration (auto-assigned Customer role)
- ✅ Login with Email + Password
- ✅ Role-based redirect (Admin → AdminDashboard, Customer → CustomerDashboard)
- ✅ Logout with session clear
- ✅ Edit Profile (Customer)

### Admin Features
- ✅ Admin Dashboard with live stats (Users, Categories, Foods, Orders, Revenue)
- ✅ User Management (CRUD + Search)
- ✅ Category Management (CRUD + Search)
- ✅ Food Item Management (CRUD + Search)
- ✅ Order Management (View all + Update Status)
- ✅ Revenue tracking

### Customer Features
- ✅ Customer Dashboard with order stats
- ✅ Browse Menu with Category Filter + Search
- ✅ Cart (Add, Remove, Increase, Decrease, Clear)
- ✅ Checkout with Payment Method selection
- ✅ My Orders with progress bar
- ✅ Order Status Tracking

### Beyond CRUD Functionalities
1. ✅ **Advanced Search & Filtering** — Search by name/email/role across all modules + category filter in menu
2. ✅ **In-App Notifications** — Bell icon with unread count, sent on order placed and status changes
3. ✅ **Payment / Billing** — Multiple payment methods (Cash on Delivery, bKash, Nagad, Card)
4. ✅ **Workflow Automation** — Status changes (Pending → Preparing → Delivered) auto-trigger notifications
5. ✅ **Cart System** — Session-based cart with JSON serialization, real-time total calculation
6. ✅ **Role-Based Access Control** — Admin and Customer see different dashboards and navigation

---

## 🛠️ Tech Stack

| Technology | Purpose |
|------------|---------|
| ASP.NET Core MVC (.NET 10) | Web Framework |
| Entity Framework Core 10 | ORM / Database Interaction |
| SQL Server (SQLEXPRESS) | Relational Database |
| AutoMapper 14 | Object-to-Object Mapping |
| Bootstrap 5 | UI Framework |
| Font Awesome 6.5 | Icons |
| Session + JSON | Cart Storage |
| MailKit | Email Notifications |
| Twilio | SMS Notifications |

---

## 🗄️ Database Schema

```sql
users
├── id (PK)
├── name
├── email (UNIQUE)
├── password
└── role (Admin / Customer)

categories
├── id (PK)
├── name
└── description

foods
├── id (PK)
├── name
├── description
├── price
├── image
└── category_id (FK → categories)

orders
├── id (PK)
├── user_id (FK → users)
├── order_date
├── status (Pending/Preparing/Delivered/Cancelled)
├── total
├── payment_method
└── payment_status

order_items
├── id (PK)
├── order_id (FK → orders)
├── food_id (FK → foods)
├── food_name
├── quantity
└── price

notifications
├── id (PK)
├── user_id (FK → users)
├── title
├── message
├── is_read
└── created_at
```

### Relationships
```
users     (1) ──── (many) orders
users     (1) ──── (many) notifications
categories(1) ──── (many) foods
orders    (1) ──── (many) order_items
foods     (1) ──── (many) order_items
```

---

## 📁 Project Structure

```
OnlineFoodOrderingSystem/
│
├── DAL/                          # Data Access Layer
│   ├── EF/
│   │   ├── Tables/               # Entity classes
│   │   │   ├── User.cs
│   │   │   ├── Category.cs
│   │   │   ├── Food.cs
│   │   │   ├── Order.cs
│   │   │   ├── OrderItem.cs
│   │   │   └── Notification.cs
│   │   └── OnlineFoodOrderingSystemDbContext.cs
│   └── Repos/                    # Repository classes
│       ├── UserRepo.cs
│       ├── CategoryRepo.cs
│       ├── FoodRepo.cs
│       ├── OrderRepo.cs
│       └── NotificationRepo.cs
│
├── BAL/                          # Business Logic Layer
│   ├── DTOs/                     # Data Transfer Objects
│   │   ├── UserDTO.cs
│   │   ├── LoginDTO.cs
│   │   ├── RegisterDTO.cs
│   │   ├── CategoryDTO.cs
│   │   ├── FoodDTO.cs
│   │   ├── OrderDTO.cs
│   │   ├── OrderItemDTO.cs
│   │   ├── CartItemDTO.cs
│   │   ├── CheckoutDTO.cs
│   │   └── NotificationDTO.cs
│   ├── Services/                 # Business Logic
│   │   ├── UserService.cs
│   │   ├── CategoryService.cs
│   │   ├── FoodService.cs
│   │   ├── OrderService.cs
│   │   └── NotificationService.cs
│   └── MapperConfig.cs           # AutoMapper Configuration
│
└── APP/                          # Presentation Layer
    ├── Controllers/
    │   ├── AccountController.cs
    │   ├── AdminDashboardController.cs
    │   ├── CustomerDashboardController.cs
    │   ├── UserController.cs
    │   ├── CategoryController.cs
    │   ├── FoodController.cs
    │   ├── MenuController.cs
    │   ├── CartController.cs
    │   ├── OrderController.cs
    │   ├── NotificationController.cs
    │   └── HomeController.cs
    ├── Views/
    │   ├── Account/         (Login, Register)
    │   ├── AdminDashboard/  (Index, UserList, Create, Edit, Delete)
    │   ├── CustomerDashboard/(Index, Edit)
    │   ├── Category/        (Index, Create, Edit, Delete)
    │   ├── Food/            (Index, Create, Edit, Delete)
    │   ├── Menu/            (Index)
    │   ├── Cart/            (Index, Checkout)
    │   ├── Order/           (Index, MyOrders)
    │   ├── Notification/    (Index)
    │   ├── Home/            (Index)
    │   └── Shared/          (_Layout, _HomeLayout)
    └── Program.cs
```

---

## 🚀 Getting Started

### Prerequisites

- Visual Studio 2022+
- .NET 10 SDK
- SQL Server Express
- SQL Server Management Studio (SSMS)

### Step 1 — Clone the Repository

```bash
git clone https://github.com/yourusername/foodyy.git
cd foodyy
```

### Step 2 — Create the Database

Open SSMS and run:

```sql
CREATE DATABASE OnlineFoodOrderingSystemDB;
GO

USE OnlineFoodOrderingSystemDB;
GO

CREATE TABLE users (
    id INT PRIMARY KEY IDENTITY,
    name VARCHAR(50) NOT NULL,
    email VARCHAR(100) NOT NULL UNIQUE,
    password VARCHAR(100) NOT NULL,
    role VARCHAR(20) NOT NULL DEFAULT 'Customer'
)
GO

CREATE TABLE categories (
    id INT PRIMARY KEY IDENTITY,
    name VARCHAR(50) NOT NULL,
    description VARCHAR(255)
)
GO

CREATE TABLE foods (
    id INT PRIMARY KEY IDENTITY,
    name VARCHAR(100) NOT NULL,
    description VARCHAR(255),
    price DECIMAL(10,2) NOT NULL,
    image VARCHAR(255),
    category_id INT FOREIGN KEY REFERENCES categories(id)
)
GO

CREATE TABLE orders (
    id INT PRIMARY KEY IDENTITY,
    user_id INT FOREIGN KEY REFERENCES users(id),
    order_date DATETIME DEFAULT GETDATE(),
    status VARCHAR(50) DEFAULT 'Pending',
    total DECIMAL(10,2),
    payment_method VARCHAR(50) DEFAULT 'Cash on Delivery',
    payment_status VARCHAR(50) DEFAULT 'Paid'
)
GO

CREATE TABLE order_items (
    id INT PRIMARY KEY IDENTITY,
    order_id INT FOREIGN KEY REFERENCES orders(id),
    food_id INT FOREIGN KEY REFERENCES foods(id),
    food_name VARCHAR(100),
    quantity INT,
    price DECIMAL(10,2)
)
GO

CREATE TABLE notifications (
    id INT PRIMARY KEY IDENTITY,
    user_id INT FOREIGN KEY REFERENCES users(id),
    title VARCHAR(100) NOT NULL,
    message VARCHAR(255) NOT NULL,
    is_read BIT DEFAULT 0,
    created_at DATETIME DEFAULT GETDATE()
)
GO

-- Seed Data
INSERT INTO users (name, email, password, role)
VALUES ('Admin', 'admin@foodyy.com', 'admin123', 'Admin')

INSERT INTO users (name, email, password, role)
VALUES ('John', 'john@foodyy.com', 'john123', 'Customer')

INSERT INTO categories (name, description)
VALUES ('Burger', 'Delicious burgers'),
       ('Pizza', 'Fresh pizzas'),
       ('Drinks', 'Cold beverages'),
       ('Chicken', 'Fried and grilled chicken'),
       ('Desserts', 'Sweet treats')

INSERT INTO foods (name, description, price, category_id)
VALUES ('Chicken Burger', 'Juicy chicken patty with lettuce', 180.00, 1),
       ('Beef Burger', 'Classic beef with cheese', 220.00, 1),
       ('Pepperoni Pizza', 'Classic pepperoni pizza', 350.00, 2),
       ('Coca Cola', 'Cold refreshing drink 500ml', 60.00, 3),
       ('Fried Chicken', 'Crispy fried chicken', 280.00, 4),
       ('Ice Cream', 'Vanilla ice cream', 120.00, 5)
GO
```

### Step 3 — Update Connection String

In `APP/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DbConn": "Server=YOUR_SERVER_NAME\\SQLEXPRESS;Database=OnlineFoodOrderingSystemDB;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

Replace `YOUR_SERVER_NAME` with your machine name (e.g. `DESKTOP-9I7Q0RK`).

### Step 4 — Restore Packages

```bash
dotnet restore
```

### Step 5 — Run the Application

```bash
cd APP
dotnet run
```

Or press **F5** in Visual Studio.

App runs at: `https://localhost:7269`

---

## 🔑 Default Credentials

| Role | Email | Password |
|------|-------|----------|
| Admin | admin@foodyy.com | admin123 |
| Customer | john@foodyy.com | john123 |

---

## ⚙️ Functionalities Explained

### 1. Search & Filtering
```
Users   → Search by name, email, role
Categories → Search by name
Foods   → Search by name, description
Menu    → Filter by category + search by name
```

### 2. In-App Notifications
```
Trigger 1: Customer places order  → notification created
Trigger 2: Admin updates status   → notification created
Bell icon shows unread count in navbar
Notifications page shows all with mark-read option
```

### 3. Payment Methods
```
💵 Cash on Delivery
📱 bKash
📱 Nagad
💳 Credit / Debit Card
```

### 4. Workflow Automation
```
Pending → Preparing → Delivered → Cancelled
Each status change automatically:
  - Updates DB
  - Sends in-app notification to customer
```

### 5. Cart System
```
Session-based storage using JSON serialization
Add → Increase/Decrease quantity → Remove → Clear
Real-time total calculation
Checkout with payment method selection
```

### 6. Role-Based Access
```
Admin  → AdminDashboard, Users, Categories, Foods, Orders
Customer → CustomerDashboard, Menu, Cart, MyOrders, Profile
```

---

## 🌐 HTTP Methods Used

| Method | Usage |
|--------|-------|
| `GET` | Load pages, forms, display data |
| `POST` | Submit forms, create/update/delete, checkout |

---

## 👨‍💻 Author

**Rashed mojammel**
- Email: rashedmojammel56@gmail.com
- Location: Dhaka, Bangladesh

