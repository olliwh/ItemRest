CREATE TABLE [dbo].Items (
    [Id]     INT          IDENTITY (1, 1) PRIMARY KEY,
    [Name] VARCHAR (7)  NOT NULL,
    [Price] INT          NOT NULL
);
INSERT INTO Items (Name, Price)
VALUES ('Apple', 4)
INSERT INTO Items (Name, Price)
VALUES ('Tomato', 5)
INSERT INTO Items (Name, Price)
VALUES ('TV', 4000)


