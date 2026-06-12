````md
# Actividad de Investigación y Práctica

## Estructuras de Datos Avanzadas y APIs con ASP.NET Core

**Nombre:** Edgar Daniel Cabrera Arévalo 


---

# Parte 1: Investigación Teórica

## 1. Estructuras de Datos Eficientes

### Árboles Binarios de Búsqueda (ABB)

Un Árbol Binario de Búsqueda (ABB) es una estructura de datos jerárquica en la que cada nodo puede tener como máximo dos hijos.

#### Regla de Ordenamiento

- Los valores menores que el nodo actual se almacenan en el subárbol izquierdo.
- Los valores mayores que el nodo actual se almacenan en el subárbol derecho.

**Ejemplo:**

\```text
        10
       /  \
      5   15
\```

#### Principal Desventaja

Cuando los datos se insertan en orden secuencial, el árbol puede degenerarse y comportarse como una lista enlazada.

**Ejemplo:**

\```text
10
 \
  20
   \
    30
     \
      40
\```

En este caso, las operaciones de búsqueda, inserción y eliminación dejan de ejecutarse en O(log n) y pasan a O(n), disminuyendo considerablemente la eficiencia.

---

### Árboles AVL

Un árbol AVL es un Árbol Binario de Búsqueda auto-balanceado que mantiene equilibrada la altura de sus subárboles mediante rotaciones.

#### Factor de Balanceo

El factor de balanceo se calcula mediante la siguiente fórmula:

\```text
Factor = Altura(Izquierda) - Altura(Derecha)
\```

Un nodo se considera balanceado cuando su factor es:

\```text
-1, 0 o 1
\```

Si el factor es menor que -1 o mayor que 1, el árbol realiza rotaciones para recuperar el equilibrio.

#### Complejidad

Gracias a su balanceo automático, la altura del árbol se mantiene cercana a log₂(n), permitiendo que las operaciones principales conserven una complejidad logarítmica.

| Operación | Complejidad |
|-----------|-------------|
| Búsqueda | O(log n) |
| Inserción | O(log n) |
| Eliminación | O(log n) |

---

# 2. Fundamentos de Web APIs

## ¿Qué es una API y cómo funciona el modelo Cliente-Servidor?

Una API (Application Programming Interface) es un conjunto de reglas que permite la comunicación entre diferentes aplicaciones de software.

El modelo Cliente-Servidor funciona de la siguiente manera:

1. El cliente envía una petición (Request).
2. El servidor recibe y procesa la solicitud.
3. El servidor devuelve una respuesta (Response).

La comunicación se realiza generalmente mediante el protocolo HTTP.

### Flujo de Comunicación

\```text
Cliente
   |
   | Request HTTP
   v
Servidor / API
   |
   | Response HTTP
   v
Cliente
\```

Por ejemplo, una aplicación puede solicitar información a una API mediante una petición HTTP y recibir una respuesta en formato JSON.

---

## Verbos HTTP

### GET

#### Definición

El método GET se utiliza para recuperar información o consultar recursos existentes en el servidor.

#### Ejemplo

\```http
GET /api/nodos
\```

#### Idempotencia

GET es un método idempotente porque realizar la misma petición varias veces produce el mismo resultado y no modifica el estado del servidor.

---

### POST

#### Definición

El método POST se utiliza para crear nuevos recursos en el servidor.

#### Ejemplo

\```http
POST /api/nodos
\```

\```json
{
  "id": 15,
  "valor": "Nuevo Nodo Derecho"
}
\```

#### Idempotencia

POST no es un método idempotente porque ejecutar la misma petición varias veces puede generar múltiples recursos nuevos y modificar el estado del servidor.

---

# Parte 2: Implementación Práctica

*Esta sección será completada después de desarrollar la API en ASP.NET Core.*

---

# Parte 3: Verificación y Pruebas

Esta sección incluirá:

- Captura de la prueba GET.
- Captura de la prueba POST.
- Captura de la ejecución de la API.
- Enlace al repositorio de GitHub.
````
