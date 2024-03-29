--
-- PostgreSQL database dump
--

-- Dumped from database version 14.10 (Homebrew)
-- Dumped by pg_dump version 14.10 (Homebrew)

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: Actors; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Actors" (
    "Id" integer NOT NULL,
    "Name" character varying(100) NOT NULL,
    "Surname" character varying(100) NOT NULL,
    "BirthDate" timestamp with time zone,
    "Height" integer DEFAULT 0 NOT NULL,
    "EyeColor" character varying(50) NOT NULL,
    "Education" character varying(200) NOT NULL,
    "Languages" character varying(200) NOT NULL,
    "Skills" character varying(500) NOT NULL,
    "PolskieKinoUrl" character varying(200) NOT NULL,
    "MainImageUrl" text NOT NULL,
    "GalleryImageUrls" text[] NOT NULL,
    "Gender" text DEFAULT ''::text NOT NULL,
    "HairColor" text DEFAULT ''::text NOT NULL,
    "VideoUrl" text DEFAULT ''::text NOT NULL
);



--
-- Name: Actors_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public."Actors" ALTER COLUMN "Id" ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public."Actors_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: Admins; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Admins" (
    "Id" integer NOT NULL,
    "Username" text NOT NULL,
    "PasswordHash" text NOT NULL
);



--
-- Name: Admins_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public."Admins" ALTER COLUMN "Id" ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public."Admins_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: Photos; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Photos" (
    "PhotoId" integer NOT NULL,
    "ActorId" integer NOT NULL,
    "PhotoUrl" text NOT NULL
);



--
-- Name: Photos_PhotoId_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public."Photos" ALTER COLUMN "PhotoId" ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public."Photos_PhotoId_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: __EFMigrationsHistory; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL
);



--
-- Data for Name: Actors; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Actors" ("Id", "Name", "Surname", "BirthDate", "Height", "EyeColor", "Education", "Languages", "Skills", "PolskieKinoUrl", "MainImageUrl", "GalleryImageUrls", "Gender", "HairColor", "VideoUrl") FROM stdin;
16	Mateusz	Grabowski	1987-01-01 01:00:00+01	181	brązowy	AST Kraków	angielski, gwara śląska, gwara góralska	taniec, gitara, śpiew, rzut oszczepem, narty, łyżwy, pływanie, prawo jazdy	https://filmpolski.pl/fp/index.php?osoba=11165001	http://goldfinger.org.pl/upload/mateuszgrabowski/mateuszgrabowski1.jpg	{http://goldfinger.org.pl/upload/mateuszgrabowski/mateuszgrabowski1.jpg,http://goldfinger.org.pl/upload/mateuszgrabowski/mateuszgrabowski2.jpg,http://goldfinger.org.pl/upload/mateuszgrabowski/mateuszgrabowski3.jpg,http://goldfinger.org.pl/upload/mateuszgrabowski/mateuszgrabowski4.jpg,http://goldfinger.org.pl/upload/mateuszgrabowski/mateuszgrabowski5.jpg}	Mężczyzna	brązowy	https://www.youtube.com/watch?v=0I2SvwEubYA
17	Chrystian	Talik	1993-01-01 01:00:00+01	190	Zielone	AST wrocław	angielski, niemiecki, ukraiński	jazda konna, kickboxing, śpiew, Taniec	https://filmpolski.pl/fp/index.php?osoba=11182494	http://goldfinger.org.pl/upload/christiantalik/christiantalik1.JPG	{http://goldfinger.org.pl/upload/christiantalik/christiantalik2.JPG,http://goldfinger.org.pl/upload/christiantalik/christiantalik3.JPG,http://goldfinger.org.pl/upload/christiantalik/christiantalik4.JPG,http://goldfinger.org.pl/upload/christiantalik/christiantalik5.JPG,http://goldfinger.org.pl/upload/christiantalik/christiantalik6.JPG,http://goldfinger.org.pl/upload/christiantalik/christiantalik7.JPG,http://goldfinger.org.pl/upload/christiantalik/christiantalik8.JPG}	Mężczyzna	Blond	https://www.youtube.com/watch?v=WzP3rHcdmms  
18	Marcin	Urbanke	1992-01-01 01:00:00+01	181	brązowy	ast kraków	angielski, gwara śląska, gwara góralska	taniec, gitara, śpiew, rzut oszczepem, narty, łyżwy, pływanie, prawo jazdy	https://filmpolski.pl/fp/index.php?osoba=11165001	http://goldfinger.org.pl/upload/marcinurbanke/marcinurbanke1.png	{http://goldfinger.org.pl/upload/marcinurbanke/marcinurbanke2.JPG,http://goldfinger.org.pl/upload/marcinurbanke/marcinurbanke3.JPG,http://goldfinger.org.pl/upload/marcinurbanke/marcinurbanke4.JPG,http://goldfinger.org.pl/upload/marcinurbanke/marcinurbanke5.JPG,http://goldfinger.org.pl/upload/marcinurbanke/marcinurbanke6.JPG,http://goldfinger.org.pl/upload/marcinurbanke/marcinurbanke7.JPG,http://goldfinger.org.pl/upload/marcinurbanke/marcinurbanke8.JPG}	Mężczyzna	brązowy	https://www.youtube.com/watch?v=0I2SvwEubYA
21	Laura	pajor	1997-01-01 01:00:00+01	162	niebieski	ast wrocław	angielski, włoski	taniec, śpiew, akrobatyka, skrzypce, pianino, ukulele, pływanie, prawo jazdy 	https://www.filmpolski.pl/fp/index.php?osoba=11203454	http://goldfinger.org.pl/upload/laurapajor/laurapajor1.jpg	{http://goldfinger.org.pl/upload/laurapajor/laurapajor2.jpg,http://goldfinger.org.pl/upload/laurapajor/laurapajor3.jpg,http://goldfinger.org.pl/upload/laurapajor/laurapajor4.jpg}	Kobieta	ciemny blond	https://www.youtube.com/watch?v=E7jh0piwOCM
20	olga	madejska	1992-01-01 01:00:00+01	168	niebieskie	ast Wrocław	angielski	akrobatyka, gra w siatkówkę, jazda na łyżwach, taniec, prawo jazdy	https://filmpolski.pl/fp/index.php?osoba=11155668	http://goldfinger.org.pl/upload/olgamadejska/olgamadejska1.PNG	{http://goldfinger.org.pl/upload/olgamadejska/olgamadejska3.PNG,http://goldfinger.org.pl/upload/olgamadejska/olgamadejska4.PNG}	Kobieta	blond	https://www.youtube.com/watch?v=5PChHIUIMoU
19	PAtryk	Michalak	1993-01-01 01:00:00+01	176	zielony	ast kraków	angielski, grecki	prawo jazdy, boks	https://filmpolski.pl/fp/index.php?osoba=11157044	http://goldfinger.org.pl/upload/patrykmichalak/patrykmichalak1.jpg	{http://goldfinger.org.pl/upload/patrykmichalak/patrykmichalak2.jpg,http://goldfinger.org.pl/upload/patrykmichalak/patrykmichalak3.jpg,http://goldfinger.org.pl/upload/patrykmichalak/patrykmichalak4.jpg,http://goldfinger.org.pl/upload/patrykmichalak/patrykmichalak5.jpg,http://goldfinger.org.pl/upload/patrykmichalak/patrykmichalak6.jpeg}	Mężczyzna	brązowy	https://www.youtube.com/watch?v=4_VtPsb5pDM
24	julia	mika	1999-01-01 01:00:00+01	176	brązowy	pwsftviT	angielski	śpiew, perkusja, gitara, ukulele, pianino, piłka nożna, koszykówka, narty, prawo jazdy	https://filmpolski.pl/fp/index.php?osoba=11182372	http://goldfinger.org.pl/upload/juliamika/juliamika1.jpg	{http://goldfinger.org.pl/upload/juliamika/juliamika2.jpg,http://goldfinger.org.pl/upload/juliamika/juliamika3.jpg}	Kobieta	brązowy	https://www.youtube.com/watch?v=7Vq3d742Uko
23	anna maria	juźwin	1988-01-01 01:00:00+01	170	brązowy	ast warszawa	angielski, francuski	taniec, śpiew	https://filmpolski.pl/fp/index.php?osoba=11150425	http://goldfinger.org.pl/upload/annajuzwin/annajuzwin1.jpeg	{http://goldfinger.org.pl/upload/annajuzwin/annajuzwin2.jpeg,http://goldfinger.org.pl/upload/annajuzwin/annajuzwin4.jpeg,http://goldfinger.org.pl/upload/annajuzwin/annajuzwin5.jpeg}	Kobieta	brązowy	https://www.youtube.com/watch?v=yqjLfKl1eKg
\.


--
-- Data for Name: Admins; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Admins" ("Id", "Username", "PasswordHash") FROM stdin;
1	admingold	0e63b58b128f826512c4c06f0153a4212ce9b53bb88584faceafd91cba7924d6
\.


--
-- Data for Name: Photos; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Photos" ("PhotoId", "ActorId", "PhotoUrl") FROM stdin;
\.


--
-- Data for Name: __EFMigrationsHistory; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") FROM stdin;
20231230165628_PhotosAndActors	7.0.13
20240102120211_AddGenderColumn	7.0.13
20240103144846_ActorFloatToCm	7.0.13
20240115192148_VideoURLandHairColorAdded	7.0.13
20240116125147_VideoURLandHairColor	7.0.13
\.


--
-- Name: Actors_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Actors_Id_seq"', 24, true);


--
-- Name: Admins_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Admins_Id_seq"', 1, true);


--
-- Name: Photos_PhotoId_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Photos_PhotoId_seq"', 1, false);


--
-- Name: Actors PK_Actors; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Actors"
    ADD CONSTRAINT "PK_Actors" PRIMARY KEY ("Id");


--
-- Name: Admins PK_Admins; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Admins"
    ADD CONSTRAINT "PK_Admins" PRIMARY KEY ("Id");


--
-- Name: Photos PK_Photos; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Photos"
    ADD CONSTRAINT "PK_Photos" PRIMARY KEY ("PhotoId");


--
-- Name: __EFMigrationsHistory PK___EFMigrationsHistory; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."__EFMigrationsHistory"
    ADD CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId");


--
-- Name: IX_Photos_ActorId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_Photos_ActorId" ON public."Photos" USING btree ("ActorId");


--
-- Name: Photos FK_Photos_Actors_ActorId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Photos"
    ADD CONSTRAINT "FK_Photos_Actors_ActorId" FOREIGN KEY ("ActorId") REFERENCES public."Actors"("Id") ON DELETE CASCADE;


--
-- PostgreSQL database dump complete
--

