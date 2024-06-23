# KING ICT Akademija

Kao što je i zadano napravljen je middleware sa upravljanje proizovda, kategorija te korisnika. 
Ovaj repozitorij se može klonirati te pokrenuti u razvojnom okruženja. Ovaj zadatak je napravljen u Visual Studio razvojnom okruženju.
Cijela aplikacija je testirana na swagger-u. Te se preporuča isti za probu.

Za potrebe prijave koristi se korisnik: 
```json
{
  "username": "emilys",
  "password": "emilyspass"
}
```
Za potrebe kloniranja potrebno je kopirati i zalijepiti URL u npr. Visual Studio ili Visual Studio Code sljedeći URL: <br>
https://github.com/SimeBraica/king_ict.git

Projekt se može pokrenuti tako da kliknemo Run u visual studio te se nalazi na sljedećoj adresi: <br>
https://localhost:7002/swagger/index.html

## Sve su funkcionalnosti odrađene osim sljedećih:
 -  Unit testovi
 -  Integracijski testovi
 -  Refresh token

## API Endpoint-ovi:
### GET api/Product
```
curl -X 'GET' \
  'https://localhost:7002/api/Product' \
  -H 'accept: text/plain'
```
Za izvršavanje ove radnje korisnik mora imati svoj pripadajući JWT token zbog [Authorize] deklaracije kontrolera. Ovaj endpoint vraća sve proizvode u sljedećem formatu: 
JSON
```json
[
  {
    "title": "Essence Mascara Lash Princess",
    "price": 9.99,
    "shortDescription": "The Essence Mascara Lash Princess is a popular mascara known for its volumizing and lengthening effe",
    "images": [
      "https://cdn.dummyjson.com/products/images/beauty/Essence%20Mascara%20Lash%20Princess/1.png"
    ]
  }
] ...
```

### GET api/Product/1
```
curl -X 'GET' \
  'https://localhost:7002/api/Product/1' \
  -H 'accept: text/plain'
```
Za izvršavanje ove radnje korisnik mora imati svoj pripadajući JWT token. Ovaj endpoint vraća jedan proizvod ovisno koji smo id upisali.
Endpoint vraća proizvod u sljedećem JSON formatu:
```json
[
  {
    "id": 1,
    "title": "Essence Mascara Lash Princess",
    "description": "The Essence Mascara Lash Princess is a popular mascara known for its volumizing and lengthening effects. Achieve dramatic lashes with this long-lasting and cruelty-free formula.",
    "category": "beauty",
    "price": 9.99,
    "discountPercentage": 7.17,
    "rating": 4.94,
    "stock": 5,
    "tags": [
      "beauty",
      "mascara"
    ],
    "brand": "Essence",
    "sku": "RCH45Q1A",
    "weight": 2,
    "dimensions": {
      "width": 23.17,
      "height": 14.43,
      "depth": 28.01
    },
    "warrantyInformation": "1 month warranty",
    "shippingInformation": "Ships in 1 month",
    "availabilityStatus": "Low Stock",
    "reviews": [
      {
        "rating": 2,
        "comment": "Very unhappy with my purchase!",
        "date": "2024-05-23T08:56:21.618Z",
        "reviewerName": "John Doe",
        "reviewerEmail": "john.doe@x.dummyjson.com"
      },
      {
        "rating": 2,
        "comment": "Not as described!",
        "date": "2024-05-23T08:56:21.618Z",
        "reviewerName": "Nolan Gonzalez",
        "reviewerEmail": "nolan.gonzalez@x.dummyjson.com"
      },
      {
        "rating": 5,
        "comment": "Very satisfied!",
        "date": "2024-05-23T08:56:21.618Z",
        "reviewerName": "Scarlett Wright",
        "reviewerEmail": "scarlett.wright@x.dummyjson.com"
      }
    ],
    "returnPolicy": "30 days return policy",
    "minimumOrderQuantity": 24,
    "meta": {
      "createdAt": "2024-05-23T08:56:21.618Z",
      "updatedAt": "2024-05-23T08:56:21.618Z",
      "barcode": "9164035109868",
      "qrCode": "https://dummyjson.com/public/qr-code.png"
    },
    "images": [
      "https://cdn.dummyjson.com/products/images/beauty/Essence%20Mascara%20Lash%20Princess/1.png"
    ],
    "thumbnail": "https://cdn.dummyjson.com/products/images/beauty/Essence%20Mascara%20Lash%20Princess/thumbnail.png"
  }
]
```
### GET api/Product/filter
```
curl -X 'GET' \
  'https://localhost:7002/api/Product/filter?category=beauty&price=9.99' \
  -H 'accept: text/plain'
```
Za izvršavanje ove radnje korisnik mora imati svoj pripadajući JWT token. Ovaj endpoint vraća sve proizvode čiju smo kategoriju i cijenu definirali u tijelu zahtjeva.
Endpoint vraća proizvod u sljedećem JSON formatu:
```json
[
  {
    "id": 1,
    "title": "Essence Mascara Lash Princess",
    "description": "The Essence Mascara Lash Princess is a popular mascara known for its volumizing and lengthening effects. Achieve dramatic lashes with this long-lasting and cruelty-free formula.",
    "category": "beauty",
    "price": 9.99,
    "discountPercentage": 7.17,
    "rating": 4.94,
    "stock": 5,
    "tags": [
      "beauty",
      "mascara"
    ],
    "brand": "Essence",
    "sku": "RCH45Q1A",
    "weight": 2,
    "dimensions": {
      "width": 23.17,
      "height": 14.43,
      "depth": 28.01
    },
    "warrantyInformation": "1 month warranty",
    "shippingInformation": "Ships in 1 month",
    "availabilityStatus": "Low Stock",
    "reviews": [
      {
        "rating": 2,
        "comment": "Very unhappy with my purchase!",
        "date": "2024-05-23T08:56:21.618Z",
        "reviewerName": "John Doe",
        "reviewerEmail": "john.doe@x.dummyjson.com"
      },
      {
        "rating": 2,
        "comment": "Not as described!",
        "date": "2024-05-23T08:56:21.618Z",
        "reviewerName": "Nolan Gonzalez",
        "reviewerEmail": "nolan.gonzalez@x.dummyjson.com"
      },
      {
        "rating": 5,
        "comment": "Very satisfied!",
        "date": "2024-05-23T08:56:21.618Z",
        "reviewerName": "Scarlett Wright",
        "reviewerEmail": "scarlett.wright@x.dummyjson.com"
      }
    ],
    "returnPolicy": "30 days return policy",
    "minimumOrderQuantity": 24,
    "meta": {
      "createdAt": "2024-05-23T08:56:21.618Z",
      "updatedAt": "2024-05-23T08:56:21.618Z",
      "barcode": "9164035109868",
      "qrCode": "https://dummyjson.com/public/qr-code.png"
    },
    "images": [
      "https://cdn.dummyjson.com/products/images/beauty/Essence%20Mascara%20Lash%20Princess/1.png"
    ],
    "thumbnail": "https://cdn.dummyjson.com/products/images/beauty/Essence%20Mascara%20Lash%20Princess/thumbnail.png"
  }
]
  ```

### GET api/Product/search
```
curl -X 'GET' \
  'https://localhost:7002/api/Product/search?searchTerm=Essence%20' \
  -H 'accept: text/plain'
```
Za izvršavanje ove radnje korisnik mora imati svoj pripadajući JWT token. Ovaj endpoint vraća sve proizvode čije smo nazive definirali u tijelu zahtjeva.
Endpoint vraća proizvod u sljedećem JSON formatu:
  ```json
[
  {
    "id": 1,
    "title": "Essence Mascara Lash Princess",
    "description": "The Essence Mascara Lash Princess is a popular mascara known for its volumizing and lengthening effects. Achieve dramatic lashes with this long-lasting and cruelty-free formula.",
    "category": "beauty",
    "price": 9.99,
    "discountPercentage": 7.17,
    "rating": 4.94,
    "stock": 5,
    "tags": [
      "beauty",
      "mascara"
    ],
    "brand": "Essence",
    "sku": "RCH45Q1A",
    "weight": 2,
    "dimensions": {
      "width": 23.17,
      "height": 14.43,
      "depth": 28.01
    },
    "warrantyInformation": "1 month warranty",
    "shippingInformation": "Ships in 1 month",
    "availabilityStatus": "Low Stock",
    "reviews": [
      {
        "rating": 2,
        "comment": "Very unhappy with my purchase!",
        "date": "2024-05-23T08:56:21.618Z",
        "reviewerName": "John Doe",
        "reviewerEmail": "john.doe@x.dummyjson.com"
      },
      {
        "rating": 2,
        "comment": "Not as described!",
        "date": "2024-05-23T08:56:21.618Z",
        "reviewerName": "Nolan Gonzalez",
        "reviewerEmail": "nolan.gonzalez@x.dummyjson.com"
      },
      {
        "rating": 5,
        "comment": "Very satisfied!",
        "date": "2024-05-23T08:56:21.618Z",
        "reviewerName": "Scarlett Wright",
        "reviewerEmail": "scarlett.wright@x.dummyjson.com"
      }
    ],
    "returnPolicy": "30 days return policy",
    "minimumOrderQuantity": 24,
    "meta": {
      "createdAt": "2024-05-23T08:56:21.618Z",
      "updatedAt": "2024-05-23T08:56:21.618Z",
      "barcode": "9164035109868",
      "qrCode": "https://dummyjson.com/public/qr-code.png"
    },
    "images": [
      "https://cdn.dummyjson.com/products/images/beauty/Essence%20Mascara%20Lash%20Princess/1.png"
    ],
    "thumbnail": "https://cdn.dummyjson.com/products/images/beauty/Essence%20Mascara%20Lash%20Princess/thumbnail.png"
  }
]
  ```
### POST /api/User/login 
Dohvaća i logira te stvara novi JWT token ako je prijava uspješna.
Ovako izgleda prijava sa točnim korisničkim podacima:
```
curl -X 'POST' \
  'https://localhost:7002/api/User/login' \
  -H 'accept: */*' \
  -H 'Content-Type: application/json' \
  -d '{
  "username": "emilys",
  "password": "emilyspass",
  "expiresInMins": 30
}'
```
Te vraća sljedeći JSON odgovor:
```json
{
  "id": 1,
  "username": "emilys",
  "email": "emily.johnson@x.dummyjson.com",
  "firstName": "Emily",
  "lastName": "Johnson",
  "gender": "female",
  "image": "https://dummyjson.com/icon/emilys/128",
  "token": "{\"token\":\"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VybmFtZSI6ImVtaWx5cyIsIm5iZiI6MTcxOTE1Njg2NSwiZXhwIjoxNzE5MTU4NjY1LCJpYXQiOjE3MTkxNTY4NjUsImlzcyI6Imh0dHBzOi8vY29ubmVjdGVkUHJvZ3JhbW1lci5jb20iLCJhdWQiOiJodHRwczovL2Nvbm5lY3RlZFByb2dyYW1tZXIuY29tIn0.25SiJ2ZaVfcQd-BAyCxMCGFw7tv02_CDVJmv-sBTQZs\",\"username\":\"emilys\"}",
  "refreshToken": "0"
}
```
### GET /api/User/me
```
curl -X 'GET' \
  'https://localhost:7002/api/User/me' \
  -H 'accept: text/plain'
```
Ova API putanja vraća podatke trenutno logiranog korisnika koji ima valjani JWT token. Te vraća sljedeći JSON odgovor:
```json
{
  "id": 1,
  "firstName": "Emily",
  "lastName": "Johnson",
  "maidenName": "Smith",
  "age": 28,
  "gender": "female",
  "email": "emily.johnson@x.dummyjson.com",
  "phone": "+81 965-431-3024",
  "username": "emilys",
  "password": "emilyspass",
  "birthDate": "1996-5-30",
  "image": "https://dummyjson.com/icon/emilys/128",
  "bloodGroup": "O-",
  "height": 193.24,
  "weight": 63.16,
  "eyeColor": "Green",
  "hair": {
    "color": "Brown",
    "type": "Curly"
  },
  "ip": "42.48.100.32",
  "address": {
    "addressLine": null,
    "city": "Phoenix",
    "state": "Mississippi",
    "stateCode": "MS",
    "postalCode": "29112",
    "coordinates": {
      "lat": -77.16213,
      "lng": -92.084824
    },
    "country": "United States"
  },
  "macAddress": "47:fa:41:18:ec:eb",
  "university": "University of Wisconsin--Madison",
  "bank": {
    "cardExpire": "03/26",
    "cardNumber": "9289760655481815",
    "cardType": "Elo",
    "currency": "CNY",
    "iban": "YPUXISOBI7TTHPK2BR3HAIXL"
  },
  "company": {
    "department": "Engineering",
    "name": "Dooley, Kozey and Cronin",
    "title": "Sales Manager",
    "address": {
      "addressLine": null,
      "city": "San Francisco",
      "state": "Wisconsin",
      "stateCode": "WI",
      "postalCode": "37657",
      "coordinates": {
        "lat": 71.814525,
        "lng": -161.150263
      },
      "country": "United States"
    }
  },
  "ein": "977-175",
  "ssn": "900-590-289",
  "userAgent": "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/96.0.4664.93 Safari/537.36",
  "crypto": {
    "coin": "Bitcoin",
    "wallet": "0xb9fc2fe63b2a6c003f1c324c3bfa53259162181a",
    "network": "Ethereum (ERC20)"
  },
  "role": "admin"
}
```
### POST /api/User/logout
```
curl -X 'POST' \
  'https://localhost:7002/api/User/logout' \
  -H 'accept: */*' \
  -d ''
```
Ova putanja samo briše sve kolčiće te sadržaj u njima te tako je implementiran logout.
