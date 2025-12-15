using System;
using System.Data.SQLite;
using System.IO;

namespace ShelterManagementSystem.Data
{
    public static class DatabaseHelper
    {
        private static string DbFile = "shelter_v3.db";
        public static string ConnectionString = $"Data Source={DbFile};Version=3;";

        public static void InitializeDatabase()
        {
            if (!File.Exists(DbFile))
            {
                SQLiteConnection.CreateFile(DbFile);
                using (var conn = new SQLiteConnection(ConnectionString))
                {
                    conn.Open();
                    string sql = @"
                        -- 1. Users Table
                        CREATE TABLE Users (
                            UserID INTEGER PRIMARY KEY AUTOINCREMENT,
                            Username TEXT NOT NULL UNIQUE,
                            Password TEXT NOT NULL,
                            Role TEXT NOT NULL,
                            CONSTRAINT CHK_UserRole CHECK (Role IN ('Admin', 'Veterinarian', 'Caretaker', 'Adopter'))
                        );

                        -- 2. Cities Table
                        CREATE TABLE Cities (
                            CityID INTEGER PRIMARY KEY AUTOINCREMENT,
                            CityName TEXT NOT NULL UNIQUE,
                            ZipCode TEXT
                        );

                        -- 3. Enclosures Table
                        CREATE TABLE Enclosures (
                            EnclosureID INTEGER PRIMARY KEY AUTOINCREMENT,
                            EnclosureName TEXT NOT NULL UNIQUE,
                            Capacity INTEGER NOT NULL,
                            CityID INTEGER,
                            FOREIGN KEY (CityID) REFERENCES Cities(CityID)
                        );

                        -- 4. Animals Table
                        CREATE TABLE Animals (
                            AnimalID INTEGER PRIMARY KEY AUTOINCREMENT,
                            Name TEXT NOT NULL,
                            Species TEXT NOT NULL,
                            Gender TEXT NOT NULL,
                            AdmissionDate TEXT NOT NULL,
                            EnclosureID INTEGER NOT NULL,
                            CityID INTEGER,
                            HealthStatus TEXT DEFAULT 'Healthy',
                            AdoptionStatus TEXT DEFAULT 'Available',
                            FOREIGN KEY (EnclosureID) REFERENCES Enclosures(EnclosureID),
                            FOREIGN KEY (CityID) REFERENCES Cities(CityID)
                        );

                        -- 5. Employees Table
                        CREATE TABLE Employees (
                            EmployeeID INTEGER PRIMARY KEY AUTOINCREMENT,
                            FirstName TEXT NOT NULL,
                            LastName TEXT NOT NULL,
                            Position TEXT NOT NULL,
                            CityID INTEGER NOT NULL,
                            EnclosureID INTEGER,
                            FOREIGN KEY (CityID) REFERENCES Cities(CityID),
                            FOREIGN KEY (EnclosureID) REFERENCES Enclosures(EnclosureID)
                        );
                        
                        -- 6. Adoptions Table
                        CREATE TABLE Adoptions (
                            AdoptionID INTEGER PRIMARY KEY AUTOINCREMENT,
                            AnimalID INTEGER NOT NULL,
                            UserID INTEGER NOT NULL,
                            RequestDate TEXT NOT NULL,
                            Status TEXT DEFAULT 'Pending',
                            FOREIGN KEY (AnimalID) REFERENCES Animals(AnimalID),
                            FOREIGN KEY (UserID) REFERENCES Users(UserID)
                        );

                        -- Seed Admin User
                        INSERT INTO Users (Username, Password, Role) VALUES ('admin', 'admin123', 'Admin');
                    ";

                    using (var cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public static SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(ConnectionString);
        }
    }
}
