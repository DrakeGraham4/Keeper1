CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';
CREATE TABLE IF NOT EXISTS keeps(
  id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  name TEXT NOT NULL,
  description TEXT NOT NULL,
  img TEXT NOT NULL,
  views INT NOT NULL,
  kept INT NOT NULL,
  creatorId VARCHAR (255) NOT NULL,
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';
CREATE TABLE IF NOT EXISTS vaults(
  id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  name TEXT NOT NULL,
  description TEXT NOT NULL,
  isPrivate TINYINT NOT NULL DEFAULT 0,
  creatorId VARCHAR (255) NOT NULL,
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';
CREATE TABLE IF NOT EXISTS vaultKeeps(
  id INT AUTO_INCREMENT NOT NULL PRIMARY KEY,
  creatorId VARCHAR(255) NOT NULL,
  vaultId INT NOT NULL,
  keepId INT NOT NULL,
  FOREIGN key (creatorId) REFERENCES accounts(id) ON DELETE CASCADE,
  FOREIGN key (vaultId) REFERENCES vaults(id) ON DELETE CASCADE,
  FOREIGN key (keepId) REFERENCES keeps(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';
-- Keeps
DROP TABLE keeps;
SELECT
  *
FROM
  keeps;
SELECT
  k.*,
  a.*
FROM
  keeps k
  JOIN accounts a ON k.creatorId = a.id;
INSERT INTO
  keeps (name, description, img, views, kept, creatorId)
VALUES
  (
    'Cats',
    'All Cats',
    'hi',
    1,
    2,
    "6259a12a6ae217ab45172e68"
  );
SELECT
  *
FROM
  keeps
WHERE
  id = 3;
DELETE FROM
  keeps
WHERE
  id = 5
LIMIT
  1;
-- Vaults
  DROP TABLE vaults;
SELECT
  *
FROM
  vaults;
INSERT INTO
  vaults (name, description, isPrivate, creatorId)
VALUES
  (
    'My favorite cats',
    'awesome cats',,
    '6259a12a6ae217ab45172e68'
  );
SELECT
  v.*,
  a.*
FROM
  vaults v
  JOIN accounts a ON v.creatorId = a.id;