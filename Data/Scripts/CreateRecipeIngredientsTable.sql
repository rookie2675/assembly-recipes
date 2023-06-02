CREATE TABLE RecipeIngredients
(
    RecipeId INT,
    Ingredient NVARCHAR(100) NOT NULL,
    CONSTRAINT FK_RecipeIngredients_Recipes FOREIGN KEY (RecipeId) REFERENCES Recipes(Id) ON DELETE CASCADE
);