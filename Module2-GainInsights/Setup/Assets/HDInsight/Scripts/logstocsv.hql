INSERT OVERWRITE TABLE OutputTable 
SELECT 
CAST(get_json_object(jsonentry, "$.productid") as BIGINT) as productid,
get_json_object(jsonentry, "$.title") as title,
get_json_object(jsonentry, "$.category") as category,
get_json_object(jsonentry, "$.type") as prodtype,
CAST(get_json_object(jsonentry, "$.total") as BIGINT) as totalClicked,
CAST(get_json_object(jsonentry, "$.logdate") as int) as logdate
FROM LogsRaw
