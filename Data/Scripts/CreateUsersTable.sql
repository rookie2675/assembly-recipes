CREATE TABLE Users
(
    Id SMALLINT NOT NULL,
    Username NVARCHAR(50) NOT NULL,
    Password NVARCHAR(100) NOT NULL,
    CONSTRAINT PK_User PRIMARY KEY (Id),
    CONSTRAINT UK_User_Username UNIQUE (Username),
    CONSTRAINT CK_User_IdRange CHECK (Id >= 0 AND Id <= 65535),
    CONSTRAINT CK_User_UsernameLength CHECK (LEN(Username) >= 1 AND LEN(Username) <= 50),
    CONSTRAINT CK_User_PasswordLength CHECK (LEN(Password) >= 1 AND LEN(Password) <= 100)
);  