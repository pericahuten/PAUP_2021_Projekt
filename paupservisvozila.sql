﻿--
-- Script was generated by Devart dbForge Studio 2020 for MySQL, Version 9.0.567.0
-- Product home page: http://www.devart.com/dbforge/mysql/studio
-- Script date 6/6/2021 11:11:00 PM
-- Server version: 10.4.18
-- Client version: 4.1
--

-- 
-- Disable foreign keys
-- 
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;

-- 
-- Set SQL mode
-- 
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

-- 
-- Set character set the client will use to send SQL statements to the server
--
SET NAMES 'utf8';

DROP DATABASE IF EXISTS paupservisvozila;

CREATE DATABASE IF NOT EXISTS paupservisvozila
CHARACTER SET utf8
COLLATE utf8_general_ci;

--
-- Set default database
--
USE paupservisvozila;

--
-- Create table `ovlasti`
--
CREATE TABLE IF NOT EXISTS ovlasti (
  sifra varchar(5) NOT NULL,
  naziv varchar(255) NOT NULL,
  PRIMARY KEY (sifra)
)
ENGINE = INNODB,
AVG_ROW_LENGTH = 8192,
CHARACTER SET utf8,
COLLATE utf8_general_ci;

--
-- Create table `korisnici`
--
CREATE TABLE IF NOT EXISTS korisnici (
  korisnicko_ime varchar(30) NOT NULL,
  Ime varchar(45) NOT NULL,
  Prezime varchar(45) DEFAULT NULL,
  MobilniBroj int(10) DEFAULT NULL,
  Email varchar(100) NOT NULL,
  Lozinka varchar(255) NOT NULL,
  ovlast varchar(5) DEFAULT NULL,
  PRIMARY KEY (korisnicko_ime)
)
ENGINE = INNODB,
AVG_ROW_LENGTH = 5461,
CHARACTER SET utf8,
COLLATE utf8_general_ci;

--
-- Create foreign key
--
ALTER TABLE korisnici
ADD CONSTRAINT FK_korisnici_ovlasti FOREIGN KEY (ovlast)
REFERENCES ovlasti (sifra) ON DELETE NO ACTION;

--
-- Create table `marke_automobili`
--
CREATE TABLE IF NOT EXISTS marke_automobili (
  id int(2) NOT NULL,
  Marke varchar(35) NOT NULL,
  PRIMARY KEY (id)
)
ENGINE = INNODB,
AVG_ROW_LENGTH = 2048,
CHARACTER SET utf8,
COLLATE utf8_general_ci;

--
-- Create index `id` on table `marke_automobili`
--
ALTER TABLE marke_automobili
ADD INDEX id (id);

--
-- Create table `automobili`
--
CREATE TABLE IF NOT EXISTS automobili (
  VIN varchar(17) NOT NULL,
  KorisnikID varchar(30) NOT NULL,
  Proizvodac int(2) NOT NULL,
  Model varchar(15) NOT NULL,
  Registracija varchar(11) NOT NULL,
  PRIMARY KEY (VIN)
)
ENGINE = INNODB,
AVG_ROW_LENGTH = 1260,
CHARACTER SET utf8,
COLLATE utf8_general_ci;

--
-- Create foreign key
--
ALTER TABLE automobili
ADD CONSTRAINT FK_automobili_Proizvodac FOREIGN KEY (Proizvodac)
REFERENCES marke_automobili (id) ON DELETE NO ACTION ON UPDATE CASCADE;

--
-- Create foreign key
--
ALTER TABLE automobili
ADD CONSTRAINT FK_automobili_korisnikID FOREIGN KEY (KorisnikID)
REFERENCES korisnici (korisnicko_ime) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Create table `status`
--
CREATE TABLE IF NOT EXISTS status (
  id int(2) NOT NULL,
  Status varchar(35) NOT NULL
)
ENGINE = INNODB,
AVG_ROW_LENGTH = 5461,
CHARACTER SET utf8,
COLLATE utf8_general_ci;

--
-- Create index `id` on table `status`
--
ALTER TABLE status
ADD UNIQUE INDEX id (id);

--
-- Create table `servisi`
--
CREATE TABLE IF NOT EXISTS servisi (
  ID_Servisa int(2) NOT NULL AUTO_INCREMENT,
  autoID varchar(17) NOT NULL,
  Datum_kreiranja_zahtjeva date NOT NULL,
  Datum_pocetka_servisa date DEFAULT NULL,
  Datum_zavrsetka_servisa date DEFAULT NULL,
  Napomena_vlasnika varchar(255) DEFAULT NULL,
  Napomena_servisera varchar(255) DEFAULT NULL,
  Kilometraza int(6) DEFAULT NULL,
  Serviser varchar(55) DEFAULT NULL,
  Status int(2) NOT NULL,
  PRIMARY KEY (ID_Servisa)
)
ENGINE = INNODB,
AUTO_INCREMENT = 57,
AVG_ROW_LENGTH = 2340,
CHARACTER SET utf8,
COLLATE utf8_general_ci;

--
-- Create foreign key
--
ALTER TABLE servisi
ADD CONSTRAINT FK_autoID_VIN FOREIGN KEY (autoID)
REFERENCES automobili (VIN) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Create foreign key
--
ALTER TABLE servisi
ADD CONSTRAINT FK_servisi_Status FOREIGN KEY (Status)
REFERENCES status (id) ON DELETE NO ACTION ON UPDATE CASCADE;

--
-- Create table `racun`
--
CREATE TABLE IF NOT EXISTS racun (
  id int(5) NOT NULL AUTO_INCREMENT,
  servis int(2) NOT NULL,
  Datum date DEFAULT NULL,
  Izdaje_osoba varchar(50) NOT NULL,
  Cijena int(6) NOT NULL,
  PRIMARY KEY (id)
)
ENGINE = INNODB,
AUTO_INCREMENT = 8,
AVG_ROW_LENGTH = 4096,
CHARACTER SET utf8,
COLLATE utf8_general_ci;

--
-- Create foreign key
--
ALTER TABLE racun
ADD CONSTRAINT FK_servis_IDServisa FOREIGN KEY (servis)
REFERENCES servisi (ID_Servisa) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Create table `artikli`
--
CREATE TABLE IF NOT EXISTS artikli (
  id int(11) NOT NULL AUTO_INCREMENT,
  naziv varchar(35) NOT NULL,
  jedinica_mjere varchar(5) NOT NULL,
  cijena int(5) NOT NULL,
  PRIMARY KEY (id)
)
ENGINE = INNODB,
AUTO_INCREMENT = 7,
AVG_ROW_LENGTH = 4096,
CHARACTER SET utf8,
COLLATE utf8_general_ci;

--
-- Create table `racun_stavke`
--
CREATE TABLE IF NOT EXISTS racun_stavke (
  id int(5) NOT NULL AUTO_INCREMENT,
  id_racuna int(5) NOT NULL,
  redni_broj_stavke int(5) NOT NULL,
  sifra_artikla int(11) NOT NULL,
  kolicina int(11) NOT NULL,
  iznos int(11) DEFAULT NULL,
  PRIMARY KEY (id)
)
ENGINE = INNODB,
AUTO_INCREMENT = 36,
AVG_ROW_LENGTH = 1092,
CHARACTER SET utf8,
COLLATE utf8_general_ci;

--
-- Create index `FK_racun_stavke_id_racuna` on table `racun_stavke`
--
ALTER TABLE racun_stavke
ADD INDEX FK_racun_stavke_id_racuna (id_racuna);

--
-- Create index `FK_racun_stavke_sifra_artikla` on table `racun_stavke`
--
ALTER TABLE racun_stavke
ADD INDEX FK_racun_stavke_sifra_artikla (sifra_artikla);

--
-- Create index `IDX_racun_stavke_id_racuna` on table `racun_stavke`
--
ALTER TABLE racun_stavke
ADD INDEX IDX_racun_stavke_id_racuna (id_racuna);

--
-- Create foreign key
--
ALTER TABLE racun_stavke
ADD CONSTRAINT FK_id_racuna__racun_id FOREIGN KEY (id_racuna)
REFERENCES racun (id) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Create foreign key
--
ALTER TABLE racun_stavke
ADD CONSTRAINT FK_racun_stavke_sifra_artikla2 FOREIGN KEY (sifra_artikla)
REFERENCES artikli (id) ON DELETE NO ACTION ON UPDATE CASCADE;

-- 
-- Dumping data for table ovlasti
--
INSERT INTO ovlasti VALUES
('AD', 'Administrator'),
('US', 'Korisnik');

-- 
-- Dumping data for table marke_automobili
--
INSERT INTO marke_automobili VALUES
(0, 'Citroen'),
(1, 'Audi'),
(2, 'BMW'),
(3, 'Mercedes'),
(4, 'Mazda'),
(5, 'Opel'),
(6, 'Peugeot'),
(7, 'VW'),
(8, 'Toyota');

-- 
-- Dumping data for table korisnici
--
INSERT INTO korisnici VALUES
('Ivovic', 'Ivo', 'Ivić', 952314235, 'asdttt@gmail.com', '+/s4bv6mfoFvLdoKjJSpjrIDdXrrs/VfGDdVoZLURGc=', 'US'),
('testuser', 'test', 'user', 1234567890, 'testuser@gmail.com', '7NcYcNGWMxapfjrDQIyYNa2M8PPBvHA1J8MCZVNPda4=', 'US'),
('user111', 'Pero', 'Perić', 952152345, 'peroperic@gmail.com', 'jUPY60RIRBTWGhhlm0Q/v+UjmVENpGidU1K9ljHGxRs=', 'AD');

-- 
-- Dumping data for table status
--
INSERT INTO status VALUES
(1, 'Zahtjev kreiran'),
(2, 'Vozilo servisirano'),
(3, 'Zahtjev storniran');

-- 
-- Dumping data for table automobili
--
INSERT INTO automobili VALUES
('1C3EL56R34N349120', 'user111', 4, 'RX-8', 'XD-123-AS'),
('1FMCU9G98FUA49793', 'user111', 5, 'blyat', '1234123'),
('1G1AP14PX67607352', 'user111', 1, 'asdfasdf', 'asdf12'),
('1HGCG5644WA047116', 'user111', 1, 'M3', 'VŽ-356-CS'),
('1HTLKTVR4HH508441', 'user111', 2, 'M3', 'VŽ-356-CS'),
('1HVBRAAP12B989654', 'user111', 3, 'S class', 'XD-123-AS'),
('3GNFK16R2WG133743', 'user111', 1, 'RS6', 'XD-123-AS'),
('3LN6L2G98DR835654', 'user111', 2, 'asdfasdf', 'XD-123-AS'),
('4V4MC9GFX7N400060', 'Ivovic', 5, 'asda', '123asd'),
('5N1AR18WX5C757630', 'user111', 4, 'rx2', 'asdasd'),
('5NPDH4AE9FH554550', 'testuser', 2, 'M3', 'asdf12'),
('JM1BL1UG9C1505353', 'user111', 3, 'AMG', 'VŽ-356-CS'),
('KMHGC4DD8EU226552', 'Ivovic', 4, 'MX-5', 'VŽ-321-FS');

-- 
-- Dumping data for table servisi
--
INSERT INTO servisi VALUES
(49, '1HGCG5644WA047116', '2021-05-29', '0001-01-01', '0001-01-01', 'Proba 1', NULL, 0, 'Nedodjeljen', 3),
(50, 'KMHGC4DD8EU226552', '2021-05-29', '0001-01-01', '0001-01-01', 'ASD1', NULL, 0, 'Nedodjeljen', 3),
(51, '3LN6L2G98DR835654', '2021-05-29', '0001-01-01', '0001-01-01', 'Proba 1', NULL, 0, 'Nedodjeljen', 1),
(52, '1FMCU9G98FUA49793', '2021-06-05', '2021-06-07', '0001-01-01', 'asdf', NULL, 0, 'Nedodjeljen', 1),
(53, '1G1AP14PX67607352', '2021-06-05', '2021-06-07', '0001-01-01', 'Proba 1', NULL, 0, 'Nedodjeljen', 1),
(54, '1G1AP14PX67607352', '2021-06-05', '2021-06-03', '0001-01-01', 'Proba 1', NULL, 0, 'Nedodjeljen', 1),
(55, '5NPDH4AE9FH554550', '2021-06-05', '2021-06-10', '0001-01-01', 'test dodavanje', NULL, 0, 'Nedodjeljen', 3),
(56, 'KMHGC4DD8EU226552', '2021-06-05', '2021-06-11', '0001-01-01', 'adsffasdf', NULL, 0, 'Nedodjeljen', 1);

-- 
-- Dumping data for table racun
--
INSERT INTO racun VALUES
(4, 49, '2021-06-05', 'Perić Pero', 100),
(5, 50, '2021-06-05', 'Perić Pero', 530),
(6, 55, '2021-06-05', 'Perić Pero', 65),
(7, 49, '2021-06-05', 'Perić Pero', 30);

-- 
-- Dumping data for table artikli
--
INSERT INTO artikli VALUES
(1, 'Ulje', 'l', 15),
(2, 'Filter', 'kom', 55),
(4, 'Filter za gorivo', 'kom', 25),
(5, 'Test123', 'kom', 12);

-- 
-- Dumping data for table racun_stavke
--
INSERT INTO racun_stavke VALUES
(1, 5, 1, 2, 2, 110),
(13, 5, 2, 1, 2, 30),
(14, 5, 3, 2, 5, 275),
(15, 5, 4, 1, 1, 15),
(16, 5, 5, 1, 1, 15),
(17, 5, 6, 2, 1, 55),
(19, 5, 7, 1, 1, 15),
(22, 4, 1, 2, 1, 55),
(23, 4, 2, 1, 1, 15),
(28, 6, 1, 4, 2, 50),
(29, 6, 2, 1, 1, 15),
(30, 7, 1, 1, 2, 30),
(31, 5, 8, 1, 1, 15),
(33, 4, 3, 1, 1, 15),
(34, 4, 4, 1, 1, 15);

-- 
-- Restore previous SQL mode
-- 
/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;

-- 
-- Enable foreign keys
-- 
/*!40014 SET FOREIGN_KEY_CHECKS = @OLD_FOREIGN_KEY_CHECKS */;