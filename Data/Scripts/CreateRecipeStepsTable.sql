CREATE TABLE RecipeSteps
(
    RecipeId INT,
    Step NVARCHAR(MAX) NOT NULL,
    CONSTRAINT FK_RecipeSteps_Recipes FOREIGN KEY (RecipeId) REFERENCES Recipes(Id) ON DELETE CASCADE
);  