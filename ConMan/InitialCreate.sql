CREATE TABLE "events" (
    "id" SERIAL NOT NULL UNIQUE,
    "name" VARCHAR(100) NOT NULL,
    "description" VARCHAR(255),
    "date" DATE,
    "location" VARCHAR(255),
    PRIMARY KEY("id")
);


CREATE TABLE "participants" (
    "id" SERIAL NOT NULL UNIQUE,
    "name" VARCHAR(100) NOT NULL,
    "email" VARCHAR(255) NOT NULL UNIQUE,
    "phone" VARCHAR(18) NOT NULL UNIQUE,
    PRIMARY KEY("id")
);


CREATE TABLE "registrations" (
    "id" SERIAL NOT NULL UNIQUE,
    "event_id" INTEGER NOT NULL,
    "participant_id" INTEGER NOT NULL,
    "status" VARCHAR(20) NOT NULL,
    PRIMARY KEY("id")
);


ALTER TABLE "registrations"
ADD FOREIGN KEY("event_id") REFERENCES "events"("id")
ON UPDATE NO ACTION ON DELETE NO ACTION;
ALTER TABLE "registrations"
ADD FOREIGN KEY("participant_id") REFERENCES "participants"("id")
ON UPDATE NO ACTION ON DELETE NO ACTION;