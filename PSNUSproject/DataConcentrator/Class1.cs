using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataConcentrator
{
    public class Class1
    {
        //na ekranu: digital/analog input/output tabovi (4), alarm list, i alarm history (ukupno 6)
        //CRUD, na details alarm
        //output tag vezan na 1 adresu, inputi mogu vise
        //povezemo alarme sa tagovima
        //jedno dugme za dodavanje tagova, pa se bira tip taga (di, do, ai, ao)
        //simulator vec postoji, mi pravimo recnik koji vezuje adrese sa input/outputima u simulator
        //mi mozemo sami rucno da hard-code koja adresa se odnosi na analogne inpute/digitalne outpute
        //mozemo promeniti simulator
        //npr kazemo adrese od 0 do 5 su analogni inputi,
        //a od 6 do 10 su digitalni inputi itd.

        //outpute samo dodajemo u listu, ne moramo da simuliramo nista
        //scan-time, na koliko vremena citamo vrednost - svaki input citamo pomoci NITI
        //moracemo da imamo kolekciju svih niti (DICTIONARY<string,Thread>) kojim kontrolisemo stvari poput
        //gasenje niti kad obrisemo nesto iz tabele (ne moze onaj background nit pristup)
        //u realnom sistemu, postojao bi ThreadPool jer se ovo zakomplikuje za 10000 niti
        //kad procitamo vrednost, proverimo da li se desio neki alarm vezan za tu vrednost
        //NAPRAVITI DOKUMENTACIJU
        //low-high opseg za velicinu NIJE ISTO STO I ALARM, alarmi u podskupu opsega, ako dodje
        //nesto van opsega, zaokruzimo unutar opsega (za opseg 0-100, ako stigne 101 postavimo na 100)
        //alarmi trebaju imati prioritete, ako postoji alarm za 90, 100 i 110, ako je vrednost 112, treba
        //da obavestimo operatera samo o ovom najprioritetnijem (110)
        //operater moze da pauzira neki alarm, da mu ne iskace konstantno
        //potencijalno resenje: tag da ima "status", koji ako je alarman se ispise alarm jednom,
        //pa se tek izmeni kad prestane alarm ili se opet promeni stanje na alarm, tkd alarm stize
        //samo prvi put
        //ove promene sve preko eventova
        //pocetkom jula, krajem jula i posle pauze (manje bodova posle pauze)
        //stablo nasledjivanja - tag nasledjuje inputoutput nasledjuje digitalanalog itd., trebace klase za sve
        //konfiguracija - sadrzi sve tagove koji su u sistemu, cuvamo u xml ili bazi,
        //ne mora da sadrzi trenutnu vrednost, to se negde drugde popunjava
        //.NET resi konfiguraciju koriscenjem SERIJALIZACIJE (sam izgenerise xml semu)
        //po potrebi menjati simulator, narocito vezano za adrese
        //DataConcentrator - importujemo, pozivamo odavde metode, ovde dodajemo logiku
        //GUI projekat sadrzi samo WPF deo, ostalo sve ide u Data Concentrator
        //PLC simulator
        //spojiti ove projekte (ustv su library)
        //za inpute treba thread, outpute ne
        //najbolje krenuti od analognih inputa, koji su najkomplikovaniji
        //koristiti konkurentne kolekcije, ne raditi sam lockovanje kolekcija!!!
        //scan treba beskonacna petlja, scantime, citanje vrednosti sa adrese i ako je razlicita od tekuce upisati je
        //U KONFIGURACIJI NE PISE TRENUTNA VREDNOST, ALI ONA TREBA DA POSTOJI
        //opcija: dodati je u tag klasu koja nasledjuje sve ostale, pa je ignorisemo kad pravimo xml
        //opcija: napraviti wrapper klasu koja ima kompoziciju sa tagom, i trenutnom vrednosti
        //tag nasledjuje input tag, output tag, koje nasledjuju digital input, analog input
        //napravimo genericku klasu TagValue<T>, koja ima polje Tag tag, T Value
        //ovo radimo jer nam nekad treba vrednost koja je bool, a nekad koja je double
        //pitati vukmirovica da odma upise ocenu posle usmenog
        //PRIJAVITI PROJEKAT ASISTENTU PREKO TEAMSA!!! NAPISATI DA RADIM SAM!!!
    }
}
