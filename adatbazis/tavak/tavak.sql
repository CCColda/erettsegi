-- 2morotva
SELECT nev, terulet
FROM alloviz
WHERE nev LIKE "*morotva*"
ORDER BY terulet DESC;

-- 3vizarany
SELECT SUM(terulet)/93030 AS vizarany
FROM alloviz;

-- 4kozepes
SELECT nev
FROM alloviz
WHERE terulet BETWEEN 3 AND 10
	AND vizgyujto > terulet * 10;

-- 5sok (where)
SELECT alloviz.nev, COUNT(helykapcs.gpsid) AS telepulesek_szama
FROM alloviz, helykapcs
WHERE alloviz.id = helykapcs.allovizid
GROUP BY alloviz.nev
HAVING COUNT(helykapcs.gpsid) > 3;

-- 6keletnyugat (where)
SELECT TOP 1 alloviz.nev, MAX(hosszusag) - MIN(hosszusag) AS kulonbseg
FROM telepulesgps, helykapcs, alloviz
WHERE helykapcs.allovizid = alloviz.id
	AND helykapcs.gpsid = telepulesgps.id
GROUP BY alloviz.nev
ORDER BY MAX(hosszusag) - MIN(hosszusag) DESC;

-- 7egyegy
SELECT alloviz.nev, terulet, telepulesgps.nev
FROM alloviz, helykapcs, telepulesgps
WHERE alloviz.id=allovizid And telepulesgps.id=gpsid
	AND allovizid IN (
		SELECT allovizid FROM helykapcs GROUP BY allovizid HAVING COUNT(gpsid)=1
	)
	AND gpsid IN (
		SELECT gpsid FROM helykapcs GROUP BY gpsid HAVING COUNT(allovizid)=1
	);

-- 8tipus (segéd)
SELECT tipus, nev, terulet
FROM alloviz
WHERE tipus IS NOT NULL;




-- 5sok (inner join)
SELECT alloviz.nev, COUNT(helykapcs.gpsid) AS telepulesek_szama
FROM alloviz INNER JOIN helykapcs ON alloviz.id = helykapcs.allovizid
GROUP BY alloviz.nev
HAVING COUNT(helykapcs.gpsid) > 3;

-- 6keletnyugat (inner join)
SELECT TOP 1 alloviz.nev, MAX(hosszusag) - MIN(hosszusag) AS kulonbseg
FROM telepulesgps INNER JOIN (
	helykapcs INNER JOIN alloviz ON helykapcs.allovizid = alloviz.id
) ON helykapcs.gpsid = telepulesgps.id
GROUP BY alloviz.nev
ORDER BY MAX(hosszusag) - MIN(hosszusag) DESC;
