import csv
import psycopg2

# Connect to your PostgreSQL database
conn = psycopg2.connect(
    dbname="RestfulAPIdb",
    user="postgres",
    password="password123",
    host="localhost",
    port="5432"
)
cursor = conn.cursor()

# Open your CSV file
with open('bergen_addresses.csv', newline='', encoding='utf-8') as csvfile:
    reader = csv.DictReader(csvfile)
    
    for row in reader:
        cursor.execute("""
    INSERT INTO "Addresses" ("Latitude", "Longitude", "Street", "Housenumber", "Postcode", "City", "Country")
    VALUES (%s, %s, %s, %s, %s, %s, %s)
""", (
    row['latitude'],
    row['longitude'],
    row['street'],
    row['housenumber'],
    row['postcode'],
    row['city'],
    row['country']
))


conn.commit()
cursor.close()
conn.close()
