CREATE DATABASE Library;
GO

USE Library;
GO

CREATE TABLE Authors (
    AuthorID INT PRIMARY KEY IDENTITY,
    AuthorName NVARCHAR(100) NOT NULL
);
GO

CREATE TABLE Books (
    BookID INT PRIMARY KEY IDENTITY,
    Title NVARCHAR(255) NOT NULL,
    AuthorID INT FOREIGN KEY REFERENCES Authors(AuthorID),
    PublicationYear INT
);
GO

INSERT INTO Authors (AuthorName) VALUES
('Leo Tolstoy'),
('Fyodor Dostoevsky'),
('Jane Austen'),
('Charles Dickens'),
('Mark Twain'),
('Virginia Woolf'),
('Ernest Hemingway'),
('George Orwell'),
('Agatha Christie'),
('J.K. Rowling');
GO



INSERT INTO Books (Title, AuthorID, PublicationYear) VALUES
('War and Peace', 1, 1869),
('Anna Karenina', 1, 1877),
('Crime and Punishment', 2, 1866),
('The Idiot', 2, 1869),
('Pride and Prejudice', 3, 1813),
('Sense and Sensibility', 3, 1811),
('David Copperfield', 4, 1850),
('Great Expectations', 4, 1861),
('Adventures of Huckleberry Finn', 5, 1884),
('The Adventures of Tom Sawyer', 5, 1876),
('Mrs Dalloway', 6, 1925),
('To the Lighthouse', 6, 1927),
('The Old Man and the Sea', 7, 1952),
('A Farewell to Arms', 7, 1929),
('1984', 8, 1949),
('Animal Farm', 8, 1945),
('Murder on the Orient Express', 9, 1934),
('Death on the Nile', 9, 1937),
('Harry Potter and the Sorcerer’s Stone', 10, 1997),
('Harry Potter and the Chamber of Secrets', 10, 1998),
('Harry Potter and the Prisoner of Azkaban', 10, 1999),
('Harry Potter and the Goblet of Fire', 10, 2000),
('Harry Potter and the Order of the Phoenix', 10, 2003),
('Harry Potter and the Half-Blood Prince', 10, 2005),
('Harry Potter and the Deathly Hallows', 10, 2007);
GO
