# Exchange API

- **Zapytanie o listę (tablicę symboli) dostępnych walut**

```
host/list
```


- **Zapytanie o średni kurs waluty względem PLN**

```
host/rate/{currency}
```

Parametry:  
currency - symbol waluty


- **Zapytanie o przeliczenie kwoty między walutami**

```
host/calculate?from={currency_in}&to={currency_out}&amount={amount}
```

Parametry:  
currency_in - symbol waluty wejściowej  
currency_out - symbol waluty wyjściowej  
amount - kwota wejściowa  