DROP TABLE IF EXISTS LogsRaw;
CREATE EXTERNAL TABLE LogsRaw (jsonentry string) 
PARTITIONED BY (year INT, month INT, day INT)
STORED AS TEXTFILE LOCATION "wasb://partsunlimited@<StorageAccountName>.blob.core.windows.net/logs/"

ALTER TABLE LogsRaw ADD IF NOT EXISTS PARTITION (year=2016, month=07, day=03) LOCATION 'wasb://partsunlimited@<StorageAccountName>.blob.core.windows.net/logs/2016/07/03';
ALTER TABLE LogsRaw ADD IF NOT EXISTS PARTITION (year=2016, month=07, day=04) LOCATION 'wasb://partsunlimited@<StorageAccountName>.blob.core.windows.net/logs/2016/07/04';
ALTER TABLE LogsRaw ADD IF NOT EXISTS PARTITION (year=2016, month=07, day=05) LOCATION 'wasb://partsunlimited@<StorageAccountName>.blob.core.windows.net/logs/2016/07/05';
ALTER TABLE LogsRaw ADD IF NOT EXISTS PARTITION (year=2016, month=07, day=06) LOCATION 'wasb://partsunlimited@<StorageAccountName>.blob.core.windows.net/logs/2016/07/06';

SELECT CAST(get_json_object(jsonentry, "$.logdate") as BIGINT),
	CAST(get_json_object(jsonentry, "$.productid") as BIGINT),
	 get_json_object(jsonentry, "$.title"),
	 get_json_object(jsonentry, "$.category"),
	 get_json_object(jsonentry, "$.type"),
	 CAST(get_json_object(jsonentry, "$.total") as BIGINT)
FROM LogsRaw;