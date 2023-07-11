CREATE TABLE RecipeIngredients
(
    RecipeId BIGINT NOT NULL,
    Ingredient NVARCHAR(20) NOT NULL,
    CONSTRAINT FK_RecipeIngredients_Recipes FOREIGN KEY (RecipeId) REFERENCES Recipes(Id) ON DELETE CASCADE
);