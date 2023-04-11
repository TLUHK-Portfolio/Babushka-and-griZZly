# rif21-MM-praktika-2

Multimeediumi praktika eesmärgiks on luua arvutimäng, mille graafika ning heli on enda loodud.

## :raised_hand_with_fingers_splayed:	 Meeskond "Team 2"

| **Projektijuht** | Kerli Loopman             |
|------------------|---------------------------|
| **Arendajad**    | Kairo Luha, Kaius Karon   |
| **Disainerid**   | Kerli Loopman, Renat Ränk |


## Navigatsioon repositooriumis

Repositooriumis on järngevad kaustad:
[Helid](https://github.com/tluhk/rif21-MM-praktika-2/tree/master/Helid) - Sellest kaustast leiad kõik meie loodud heliefektid ja täpsema info leiad selle kausta readme.md-st. 
[Kujunduselemendid](https://github.com/tluhk/rif21-MM-praktika-2/tree/master/Kujunduselemendid) - Kujunduselemendid sisaldavad tööfaile ning ka mängumootorisse imporditavaid PNG faile ja ka Blenderis loodud animatsioone. Täpsema info leiad selle kausta readme.md-st. 
[Mängumootori failid](https://github.com/tluhk/rif21-MM-praktika-2/tree/master/griZZly) - siin on kogu vajalik sisu mängu jaoks.
[praktika-failid](https://github.com/tluhk/rif21-MM-praktika-2/tree/master/praktika-failid) - sellest kaustast leiad faile, mis ei ole kujunduse, heli ega arendusega seotud, kuid sisaldavad olulist infot praktika läbimise kohta.

## :bookmark_tabs: Ressurssid

[Google Drive](https://drive.google.com/drive/folders/12y-jqBrefYDzp4aK-Ckxdw56SnxjDHFl?usp=share_link) | [Inspiratsioonitahvel](https://github.com/tluhk/rif21-MM-praktika-2/blob/master/praktika-failid/Moodboard%20(1).pdf) | [Tegevuskava](https://github.com/orgs/tluhk/projects/16/views/3) | [Koosolekute memod](https://github.com/tluhk/rif21-MM-praktika-2/blob/master/praktika-failid/koosolekud.md)

# :video_game: Babushka & griZZly mängukontseptsioon

griZZly on kurjakuulutav karu, kes ühel päeval hakkab endast ohtu kujutama metsamajas elavale vanaemale (Babushka). Babushka on valmis griZZlyga võitlema, kuid tema laskemoonaks on kõik käepärane nagu moosipurgid ja molotovi kokteilid.  
Selles 2D tulistamismängus aitad sina vanaemal oma kindlust kaitsta, olles sihtimisel täpne ning tehes strateegiliselt arukaid otsuseid. Pea meeles, et ka griZZly saab vastu rünnata. Laskmine käib korda-mööda. Karu ja mutike on statsionaarsed. Karu viskab esimesel tasemel kividega ja teisel pommidega. Karu viskab oma laskemoona käest, ning Babushka kasutab puude vahele asetatud pesunööri. Tegelastel on eluriba, mis väheneb vigastuste tekkimisel. Eluriba väheneb vastavalt laskemoona kahjutegurile ning kaugusele mängijast. Hoone/kindluse objektide kahjustused mõjutavad samuti eluriba. Laskemoona on mõlemal lõputult ehk mäng kestab seni kuni kummalgi saab elutase 0%.

Mäng on jagatud kaheks tasemeks. Karu, laskemoon ja tema kindlus on teisel tasemel tugevam.

Mäng on mõeldud arvutis mängimiseks ning FullHD resolutsioonis. Mängumaailma vaade on suurem, kui mänguhetkel pildis olev kaader ning kaamera liigub mööda vaadet ringi vastavalt sellele, mis hetkel on oluline. 

## Andmed

**Nimi:** Babushka&GriZZly  
**Temaatika:** *turn-based*, *shooting*  
**Graafika:** 2D, multikalaadne  
**Mängumootor:** Unity [Loe mängumootori valiku kohta siit](https://github.com/tluhk/rif21-MM-praktika-2/blob/master/praktika-failid/mangumootori-aruanne.md)    
**Vaade**: *side-view, side-scrolling*  
**Kontrollerid**: Hiir

## Graafika
Kogu kujundus on 2D ning multikalaadne. Babushka poolel on kujundus rõõmsam ning griZZly poolel süngem. Kasutame ühe objektil kahe-kolme tooniga varjutusi, gradiente.

Loodavate ja loodud elementide loetelu kirjas [siin](https://github.com/tluhk/rif21-MM-praktika-2/blob/master/Kujunduselemendid/loodav-graafika.md)

## Mängu käivitamine:
1. Selle repositooriumi parempoolsest menüüst leiad "Releases" ja viimase release avamisel on võimalik sealt leida mängu failide .zip, mis tuleb alla laadida.
2. Peale alla laadimist paki fail lahti endale sobivasse kohta
3. Lahti pakitud kaustast leiab mängu nimega "Babushka&griZZly.exe" mille käivitamisel see avatakse
4. Naudi mängu

### Kasutatud abimaterjalid

[Brackeys youtube kanal videomängude loomisest](https://www.youtube.com/@Brackeys/videos)  
[Youtube video: 2D Character Creation And Animation For Beginners - Easy Unity Tutorial](http://www.youtube.com/watch?feature=player_embedded&v=LCNt9w12fQA)  
[2D Enemy Shooting Unity Tutorial](https://www.youtube.com/watch?v=--u20SaCCow)  
[Unity Community teema - Calculating trajectory angle to hit target position](https://forum.unity.com/threads/calculating-trajectory-angle-to-hit-target-position-angle-always-90.373197/)

# Kokkulepped

- Kord nädalas koosolekud
- Githubi ülesandeid võivad lisada kõik jooksvalt ning vaatame üle koosolekutel
- Githubi ülesannetel küljes tähtaja *sprint*, *label* (graafika/arendus), *assignee*
- Release teeb Kerli, kuid koosolekul vaatame sisu koos üle
- Graafika lisame .png failina õiges mõõdus ja tööfailina (nt. afdesign) [kujunduselementide kausta](https://github.com/tluhk/rif21-MM-praktika-2/tree/master/Kujunduselemendid) 
- Commit läheb ülesandesse ja release sisse ülesande viide


