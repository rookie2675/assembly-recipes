CREATE TABLE RecipeSteps
(
    StepNumber INT NOT NULL,
    Description NVARCHAR(100) NOT NULL,
    CONSTRAINT PK_RecipeSteps PRIMARY KEY (StepNumber),
    CONSTRAINT CK_RecipeSteps_Description_Length CHECK (LEN(Description) >= 5 AND LEN(Description) <= 100)
);