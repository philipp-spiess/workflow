DROP TABLE "PROGRAMM";
DROP TABLE "TYP";
DROP TABLE "AA";
DROP TABLE "DATEN";
DROP SEQUENCE daten_seq;

CREATE TABLE "PROGRAMM"
(
  pname varchar2(50),
  path varchar2(255),
  type varchar2(255),
  i_typ_name varchar(50),
  o_typ_name varchar(50)
);

CREATE TABLE "TYP"
(
  tname varchar(50)
);

CREATE TABLE "AA"
(
  programm_pname varchar(50),
  daten_did integer
);

CREATE TABLE "DATEN"
(
  did integer,
  typ_tname varchar2(50),
  data clob 
);

CREATE SEQUENCE daten_seq;

INSERT INTO "PROGRAMM" (PNAME, PATH, TYPE, O_TYP_NAME) VALUES ('P1 - Start', 'Program1/bin/Debug/Program1.dll', 'Program1.Programm1', 'String');
INSERT INTO "PROGRAMM" (PNAME, PATH, TYPE, I_TYP_NAME) VALUES ('P2 - Ende', 'Program2/bin/Debug/Program2.dll', 'Program2.Programm2', 'String');
INSERT INTO "PROGRAMM" (PNAME, PATH, TYPE, I_TYP_NAME, O_TYP_NAME) VALUES ('P3 - StringRev', 'Program3/bin/Debug/Program3.dll', 'Program3.Programm3', 'String', 'String');

INSERT INTO "TYP" (TNAME) VALUES ('String');
INSERT INTO "TYP" (TNAME) VALUES ('Float');
INSERT INTO "TYP" (TNAME) VALUES ('Integer');