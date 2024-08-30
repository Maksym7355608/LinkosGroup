SELECT * FROM public."Addresses" WHERE "ScheduleId" IS NULL;

SELECT ad."AddressName", i."Day", i."StartTime", i."EndTime" FROM public."Addresses" AS ad
JOIN public."Schedules" as sch ON sch."Id" = ad."ScheduleId"
JOIN public."ScheduleItems" as i ON sch."Id" = i."ScheduleId"
WHERE ad."AddressName" = 'Your value';

SELECT sch."Id", COUNT(i) AS ShutdownCount FROM public."Schedules" as sch
JOIN public."ScheduleItems" as i ON sch."Id" = i."ScheduleId"
WHERE i."Day" = 0
GROUP BY sch."Id"
ORDER BY ShutdownCount DESC
LIMIT 1;

SELECT sch."Id", SUM(i."EndTime" - i."StartTime") AS ShutdownTime FROM public."Schedules" as sch
JOIN public."ScheduleItems" as i ON sch."Id" = i."ScheduleId"
WHERE i."Day" BETWEEN 1 AND 3
GROUP BY sch."Id"
ORDER BY ShutdownTime DESC
LIMIT 1;

Додавання, зміну, видалення адрес чи графіків відключень можна виконувати в самій програмі
