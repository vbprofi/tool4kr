﻿-- Adminer 4.6.3 SQLite 3 dump
drop database if exists kr;
create database kr;
use kr;

DROP TABLE IF EXISTS abo;
CREATE TABLE abo (
  id integer NOT NULL PRIMARY KEY,
  ausgabe_von integer NOT NULL,
  ausgabe_bis integer NOT NULL,
  bezahlt_am integer  NOT NULL,
  bezahlt_von integer  NOT NULL,
  bezahlt_bis integer  NOT NULL,
  bemerkung_id integer NOT NULL,
  FOREIGN KEY (bemerkung_id) REFERENCES bemerkung (id),
  FOREIGN KEY (ausgabe_bis) REFERENCES ausgabe (id) ON DELETE NO ACTION ON UPDATE NO ACTION,
  FOREIGN KEY (ausgabe_von) REFERENCES ausgabe (id) ON DELETE NO ACTION ON UPDATE NO ACTION
);


DROP TABLE IF EXISTS ausgabe;
CREATE TABLE ausgabe (
  id integer NOT NULL PRIMARY KEY,
  ausgabe integer NOT NULL,
  preis numeric NOT NULL,
  datum integer  NOT NULL
);


DROP TABLE IF EXISTS bemerkung;
CREATE TABLE bemerkung (
  id integer NOT NULL PRIMARY KEY,
  text text NOT NULL,
  datum integer  NOT NULL
);


DROP TABLE IF EXISTS kunden;
CREATE TABLE kunden (
  id integer NOT NULL PRIMARY KEY,
  firma text NOT NULL,
  vorname text NOT NULL,
  nachname text NOT NULL,
  straße text NOT NULL,
  hausnr text NOT NULL,
  plz integer NOT NULL,
  postfach text NOT NULL,
  ort text NOT NULL,
  land text NOT NULL,
  telefon text NOT NULL,
  fax text NOT NULL,
  email text NOT NULL,
  active integer NOT NULL,
  bemerkung_id integer NOT NULL,
  erstellt_am integer  NOT NULL,
  geändert_am integer  NOT NULL,
  status_id integer NOT NULL,
  FOREIGN KEY (status_id) REFERENCES status (id),
  FOREIGN KEY (bemerkung_id) REFERENCES bemerkung (id) ON DELETE NO ACTION ON UPDATE NO ACTION
);


DROP TABLE IF EXISTS rechnung;
CREATE TABLE rechnung (
  id integer NOT NULL PRIMARY KEY,
  kunden_id integer NOT NULL,
  firma text NOT NULL,
  vorname text NOT NULL,
  nachname text NOT NULL,
  straße text NOT NULL,
  hausnr text NOT NULL,
  plz integer NOT NULL,
  postfach text NOT NULL,
  ort text NOT NULL,
  land text NOT NULL,
  telefon text NOT NULL,
  fax text NOT NULL,
  email text NOT NULL,
  bemerkung_id integer NOT NULL,
  erstellt_am integer  NOT NULL,
  gesendet_am integer  NOT NULL,
  FOREIGN KEY (bemerkung_id) REFERENCES bemerkung (id) ON DELETE CASCADE ON UPDATE CASCADE,
  FOREIGN KEY (kunden_id) REFERENCES kunden (id) ON DELETE NO ACTION ON UPDATE NO ACTION
);


DROP TABLE IF EXISTS rechnungsposten;
CREATE TABLE rechnungsposten (
  id integer NOT NULL PRIMARY KEY,
  kunden_id integer NOT NULL,
  rechnung_id integer NOT NULL,
  anzahl integer NOT NULL,
  abo_id integer NOT NULL,
  kontonr integer NOT NULL,
  blz integer NOT NULL,
  iban text NOT NULL,
  institut text NOT NULL,
  kontoinhaber text NOT NULL,
  erstellt_am integer  NOT NULL,
  bemerkung_id integer NOT NULL,
  FOREIGN KEY (kunden_id) REFERENCES kunden (id) ON DELETE CASCADE,
  FOREIGN KEY (rechnung_id) REFERENCES rechnung (id) ON DELETE CASCADE,
  FOREIGN KEY (abo_id) REFERENCES abo (id) ON DELETE CASCADE,
  FOREIGN KEY (bemerkung_id) REFERENCES bemerkung (id) ON DELETE CASCADE
);

DROP TABLE IF EXISTS status;
CREATE TABLE status (
  id integer NOT NULL PRIMARY KEY,
  eintritt_am integer  NOT NULL,
  austritt_am integer  NOT NULL,
  flag integer NOT NULL
);