CREATE DATABASE UserDatabase;
USE UserDatabase;

CREATE TABLE Users (
    Id VARCHAR(36) PRIMARY KEY,
    Username VARCHAR(255) NOT NULL,
    Email VARCHAR(255) NOT NULL UNIQUE,
    Password VARCHAR(255) NOT NULL
);

CREATE TABLE UserPlan (
    Id VARCHAR(36) PRIMARY KEY,
    UserId VARCHAR(36),
    PlanId VARCHAR(36),
    FOREIGN KEY (UserId) REFERENCES Users(Id)
);

CREATE TABLE UserActivity (
    Id VARCHAR(36) PRIMARY KEY,
    UserId VARCHAR(36),
    ActivityId VARCHAR(36),
    FOREIGN KEY (UserId) REFERENCES Users(Id)
);

CREATE TABLE UserContent (
    Id VARCHAR(36) PRIMARY KEY,
    UserId VARCHAR(36),
    ContentId VARCHAR(36),
    FOREIGN KEY (UserId) REFERENCES Users(Id)
);