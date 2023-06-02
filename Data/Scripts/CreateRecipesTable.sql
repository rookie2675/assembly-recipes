CREATE TABLE Recipes
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(MAX),
    ShortDescription NVARCHAR(255),
    CONSTRAINT CK_Recipes_ShortDescription_Length CHECK (LEN(ShortDescription) <= 255)
);