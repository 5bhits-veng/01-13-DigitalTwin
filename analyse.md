# Industrial Conveyor Digital Twin

## Funktionen

### Start

*Start* bringt den Zylinder auf der linken Seite zum drehen, was die Produktion startet wobei nach und nach, bei konstanter Geschwindigkeit, das Produkt über das Rollband wandert.

### Stop

*Stop* hält den Zylinder an und somit auch das Fließband und das Produkt darauf. Es gibt hierbei keine Verrutschungen oder ähnliches.  

### Reset

*Reset* setzt sowohl die Förderbandgeschwindigkeit auf den Normalwert zurück als auch die restlichen Produktionseinstellungen. 

### Not-Halt

*Emergeny/Not-Halt* stoppt den Produktionskreislauf, mit dem Unterschied zu Stop, das man zuerst reseten muss um es wieder neu starten zu können. 

### Fördergeschwindigkeit verändern

Bei dem verändern der *Förderbandgeschwindigkeit* schlägt das Förderband fehl und das Produkt bleibt am linken Ende/ Anfang des Förderbands stecken. Sollte ein neues kommen, schlägt es das andere einfach weg, welches darauf hin weg fliegt.

### Temperaturüberwachung

Die *Temperaturüberwachung* ist nicht zu sehen, da sie weiß auf weißem Hintergrund ist. Dadurch hat man keine Informationen.

### Zusätzliches

--

## Programmstruktur

1. ConveyorMover
2. IndustrialSensor
3. ConveyorTwinData
4. DiigtalTwinLogger

## Einordnung in virtuell Engineering

1. Förderband mit Rahmen und Seitenführungen, Antriebsmotor, Werkstücke, Einlauf- und Auslaufsensor, Signalampel (Stack Light), Bedienpult (Start, Stop, Reset, Not-Halt, Geschwindigkeit) und Leitstand-Dashboard.
2. Hallenboden (FactoryFloor), Bandrahmen, Seitenführungen (Left/RightRail), das Band selbst als Fläche und das Gehäuse der Ampel. Diese Teile sind nur Form ohne eigenes Verhalten.
3. Teile mit simuliertem Verhalten:

- Transport der Werkstücke mit Physik (Rigidbody)
- Erkennung durch die Sensoren (Trigger)
- Erzeugen und Zählen der Produkte
- Erwärmung, Abkühlung und Stromaufnahme des Motors
- Zustandsautomat (Stopped, Running, Warning, Fault, Not-Halt) mit Sperren und Reset-Pflicht
- Ampel und Alarmmeldungen
Datenprotokoll
4. Vorteile bei der Entwicklung einer realen Anlage:

- Man kann Steuerungslogik und Abläufe testen, bevor es Hardware gibt (virtuelle Inbetriebnahme).
- Fehler fallen früh und günstig auf.
- Gefährliche Situationen wie Überhitzung oder Not-Halt lassen sich gefahrlos durchspielen.
- Parameter wie Geschwindigkeit, Takt und Grenzwerte könnt ihr optimieren.
- Bediener können geschult werden.
- Die protokollierten Daten lassen sich auswerten, zum Beispiel als Grundlage für vorausschauende Wartung.
5. Was noch fehlt um eine reale Anlage zu verbinden: 

- Eine Kommunkiktionsschnittstelle
- Eine Signalliste mit Variablen(Adressen, Node-IDs, Lese- und Schreibberechtigungen, etc)
- Echte Sensordaten
- Echte Kenndaten
- Ein Sicherheitskonzept(Not-Aus muss auch von der Hardware aus laufen, nicht nur in Unity)
- Regeln für Netzwerk, Zugriff, etc