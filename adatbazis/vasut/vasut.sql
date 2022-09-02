-- 3kulfold
SELECT nev, orszag
FROM allomas
WHERE orszag IS NOT NULL
ORDER BY nev ASC;

-- 480 (where)
SELECT nev, tipus, tav
FROM vonal, hely, allomas
WHERE vonal.id = hely.vonalid
	AND allomas.id = hely.allomasid
	AND vonal.id = "80"
	AND vonal.mukodo
ORDER BY tav ASC;

-- 5vonalhossz
SELECT indulasi.vonalid, indulasi.nev, veg.nev
FROM (SELECT nev, vonalid 
		FROM allomas, hely 
		WHERE allomas.id=allomasid 
		AND tav = 0
	) AS indulasi, (SELECT nev, vonalid, tav 
		FROM allomas, hely 
		WHERE allomas.id=allomasid
	) AS veg, (SELECT vonalid, Max(tav) AS maxtav
		FROM hely 
		GROUP BY vonalid
	) AS tulso
WHERE indulasi.vonalid=veg.vonalid
	AND veg.vonalid=tulso.vonalid
	AND veg.tav =tulso.maxtav;

-- 7Hatvan (where)
SELECT allomas.nev, hely.vonalid
FROM allomas, hely
WHERE allomas.id = hely.allomasid
	AND allomas.nev <> "Hatvan"
	AND hely.vonalid IN (
		SELECT vonal.id AS hatvani_vonalak
		FROM allomas, hely, vonal
		WHERE allomas.id = hely.allomasid
			AND vonal.id = hely.vonalid
			AND allomas.nev = "Hatvan"
	);

-- 8legalabb5 (where)
SELECT allomas.nev, COUNT(hely.vonalid)
FROM allomas, hely
WHERE allomas.id = hely.allomasid
GROUP BY allomas.nev
HAVING COUNT(hely.vonalid) > 5
ORDER BY COUNT(hely.vonalid) DESC;

-- 9140 (where)
SELECT allomas.nev, hely.tav
FROM allomas, hely
WHERE allomas.id =hely.allomasid
	AND hely.vonalid = "140"
	AND hely.tav > 90
	AND hely.tav <= 100;




-- 480 (inner join)
SELECT nev, tipus, tav
FROM vonal INNER JOIN (
	hely INNER JOIN allomas ON hely.allomasid = allomas.id
) ON vonal.id = hely.vonalid
WHERE vonal.id = "80" AND vonal.mukodo
ORDER BY tav ASC;

-- 7Hatvan (inner join)
SELECT allomas.nev, hely.vonalid
FROM allomas INNER JOIN hely ON allomas.id = hely.allomasid
WHERE allomas.nev <> "Hatvan"
	AND hely.vonalid IN (
		SELECT vonal.id AS hatvani_vonalak
		FROM allomas INNER JOIN (
			hely INNER JOIN vonal ON hely.vonalid = vonal.id
		) ON allomas.id = hely.allomasid
		WHERE allomas.nev = "Hatvan"
	);

-- 8legalabb5 (inner join)
SELECT allomas.nev, COUNT(hely.vonalid)
FROM allomas INNER JOIN hely ON allomas.id = hely.allomasid
GROUP BY allomas.nev
HAVING COUNT(hely.vonalid) > 5
ORDER BY COUNT(hely.vonalid) DESC;

-- 9140 (inner join)
SELECT allomas.nev, hely.tav
FROM allomas INNER JOIN hely ON allomas.id =hely.allomasid
WHERE hely.vonalid = "140"
	AND hely.tav > 90
	AND hely.tav <= 100;
