WEBAPI 2 projekt
.NET Framework 4.5.2

Databaze
Microsoft SQL Server 2014 - 12.0.2269.0 (X64) 
Express Edition (64-bit) on Windows NT 6.3 <X64> (Build 10586: )


Instalace a konfigurace

1.) vytvorit novou webovou aplikaci v IIS konzole
2.) rozzipovat tripAdv.zip do slozky webove aplikace
3.) nastavit connection string ve web.config (line 13) na Tvuj sql server
4.) upravit konstantu CONSSERVER v Content/testPage.html na Tvuj server s projektem
Ja tam mam http://localhost:49822 a ty si tam dej treba http://localhost/tripAdvisor
nebo neco podobneho. Doporucuji stejny nazev ze ktereho pak budes spoustet tuto testovaci
stranku, pac jinak by mohl nastat problem s cross domain (jeste nemam otestovano)
5.) vytvoris si databazi s nazvem OnATrip (nebo jinym jak chces, ale zmen si pak conn string)
6.) restore databaze ze zipu tripAdvdb.zip

Spusteni

http://localhost/tripAdvisor/content/testpage.html - uprav si cestu k rootu aplikace dle Sveho nastaveni

Popis:
Testovaci stranka ktera po kliknuti na jeden z prikladu (tech 8 tlacitek nahore) vygeneruje
link + data pro backend. Data a link muzes pred odeslanim upravit a pak dole kliknes na tlacitko
Send a melo by se to odeslat na server v JSON formatu. Vraci se take JSON format.

Ostatni:

Je zde zatim 8 metod ktere CRUD uzivatele a vylet. Ja budu delat na ostatnich metodach a 
behem tydne Ti poslu dalsi update projektu. Dulezite je ted abys to rozjel u Sebe na pocitaci.
Jestli mas nejake dotazy, pripominky a nebo Ti to nefacha, tak mi dej vedet.