DROP TABLE IF EXISTS OutputTable;
CREATE EXTERNAL TABLE OutputTable (
	productid int,
	title string,
	category string,
	prodtype string,
	totalclicked int,
	logdate int
) 
ROW FORMAT DELIMITED FIELDS TERMINATED BY ',' LINES TERMINATED BY '\n'
STORED AS TEXTFILE LOCATION 'wasb://processeddata@<StorageAccountName>.blob.core.windows.net/logs';


