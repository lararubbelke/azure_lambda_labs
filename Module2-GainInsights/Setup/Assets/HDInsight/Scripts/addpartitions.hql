ALTER TABLE LogsRaw ADD IF NOT EXISTS PARTITION (year=${hiveconf:Year}, month=${hiveconf:Month}, day=${hiveconf:Day}) LOCATION 'wasb://partsunlimited@${hiveconf:StorageAccountName}.blob.core.windows.net/logs/${hiveconf:Year}/${hiveconf:Month}/${hiveconf:Day}';

