#KING ICT Akademija

Kao što je i zadano napravljen je middleware sa upravljanje proizovda, kategorija te korisnika. 
Ovaj repoziroij se može klonirati te pokrenuti u razvojnom okruženja. Ovaj zadatak je napravljen u Visual Studio razvojnom okruženju.

Za potrebe prijave koristi se korisnik: 
```json
{
  "username": "emilys",
  "password": "emilyspass"
}

Za potrebe kloniranja potrebno je kopirati i zalijepiti URL u npr. Visual Studio ili Visual Studio Code sljedeći URL:
https://github.com/SimeBraica/king_ict.git

Projekt se može pokrenuti tako da kliknemo Run u visual studio te se nalazi na sljedećoj adresi:
https://localhost:7002/swagger/index.html

Sve su funkcionalnosti odrađene osim sljedećih:
  Unit testovi
  Integracijski testovi
  Refresh token

API Endpoint-ovi:
GET api/Product
curl -X 'GET' \
  'https://localhost:7002/api/Product' \
  -H 'accept: text/plain'
Za izvršavanje ove radnje korisnik mora imati svoj pripadajući JWT token. Ovaj endpoint vraća sve proizvode o sljedećem formatu: 
JSON
[
  {
    "title": "string",
    "price": 0,
    "shortDescription": "string",
    "images": [
    "string"
    ]
  }
]


GET api/Product/1
curl -X 'GET' \
  'https://localhost:7002/api/Product/1' \
  -H 'accept: text/plain'
Za izvršavanje ove radnje korisnik mora imati svoj pripadajući JWT token. Ovaj endpoint vraća jedan proizvod ovisno koji smo id upisali.
Endpoint vraća proizvod u sljedećem JSON formatu:
{
  "id": 0,
  "title": "string",
  "description": "string",
  "category": "string",
  "price": 0,
  "discountPercentage": 0,
  "rating": 0,
  "stock": 0,
  "tags": [
    "string"
  ],
  "brand": "string",
  "sku": "string",
  "weight": 0,
  "dimensions": {
    "width": 0,
    "height": 0,
    "depth": 0
  },
  "warrantyInformation": "string",
  "shippingInformation": "string",
  "availabilityStatus": "string",
  "reviews": [
    {
      "rating": 0,
      "comment": "string",
      "date": "2024-06-23T15:18:25.921Z",
      "reviewerName": "string",
      "reviewerEmail": "string"
    }
  ],
  "returnPolicy": "string",
  "minimumOrderQuantity": 0,
  "meta": {
    "createdAt": "2024-06-23T15:18:25.921Z",
    "updatedAt": "2024-06-23T15:18:25.921Z",
    "barcode": "string",
    "qrCode": "string"
  },
  "images": [
    "string"
  ],
  "thumbnail": "string"
}
