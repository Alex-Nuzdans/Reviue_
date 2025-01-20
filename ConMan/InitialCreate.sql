-- Таблица statuses
CREATE TABLE "statuses" (
    "id" SERIAL NOT NULL UNIQUE,
    "status_name" VARCHAR(50) NOT NULL UNIQUE,
    PRIMARY KEY("id")
);

-- Таблица locations
CREATE TABLE "locations" (
    "id" SERIAL NOT NULL UNIQUE,
    "location_name" VARCHAR(255) NOT NULL UNIQUE,
    "address" VARCHAR(255),
    "city" VARCHAR(100),
    "country" VARCHAR(100),
    PRIMARY KEY("id")
);

-- Таблица events
CREATE TABLE "events" (
    "id" SERIAL NOT NULL UNIQUE,
    "name" VARCHAR(100) NOT NULL,
    "description" VARCHAR(255),
    "date" DATE,
    "location_id" INTEGER NOT NULL,
    PRIMARY KEY("id"),
    
    FOREIGN KEY ("location_id") REFERENCES "locations"("id")
    ON UPDATE NO ACTION ON DELETE NO ACTION
);

-- Таблица participants
CREATE TABLE "participants" (
    "id" SERIAL NOT NULL UNIQUE,
    "name" VARCHAR(100) NOT NULL,
    "email" VARCHAR(255) NOT NULL UNIQUE,
    "phone" VARCHAR(18) NOT NULL UNIQUE,
    PRIMARY KEY("id")
);

-- Таблица registrations
CREATE TABLE "registrations" (
    "id" SERIAL NOT NULL UNIQUE,
    "event_id" INTEGER NOT NULL,
    "participant_id" INTEGER NOT NULL,
    "status_id" INTEGER NOT NULL,
    PRIMARY KEY("id"),
    
    FOREIGN KEY ("event_id") REFERENCES "events"("id")
    ON UPDATE NO ACTION ON DELETE NO ACTION,
    FOREIGN KEY ("participant_id") REFERENCES "participants"("id")
    ON UPDATE NO ACTION ON DELETE NO ACTION,
    FOREIGN KEY ("status_id") REFERENCES "statuses"("id")
    ON UPDATE NO ACTION ON DELETE NO ACTION
);