# Slither.IP Unity

## Descripción
Slither.IP es un videojuego multijugador para 2 jugadores inspirado en la mecánica de juegos como *slither.io*.  
Cada jugador controla una “serpiente” y compite por recolectar comida (bolitas) para aumentar su puntuación dentro de un tiempo límite (60 segs).

El jugador con mayor puntaje al finalizar la partida gana.

---

## Maximo de Jugadores (2)
- Jugador Azul (Azulito)
- Jugador Rojo (Rojito)

Ambos jugadores pueden:
- Verse en tiempo real
- Ver el crecimiento de la cola del otro jugador en tiempo real
- Moverse libremente por el mapa
- Competir por recolectar comida

---

## Comunicación con el servidor

El proyecto utiliza el servidor proporcionado en clase mediante peticiones HTTP que es Docker activado y funcional.

### Funcionamiento
- Cada cliente envía periódicamente:
  - Posición del jugador (Vector3)
  - Identificador del jugador (playerId)
- El cliente consulta (polling) la posición del otro jugador para mantener sincronización y se puede obser en consola como cada jugador manda un Get y un Post para mostrar que se estan viendo entre ellos.

### Tecnologías usadas
- Unity (C#)
- Peticiones HTTP (corrutinas)
- Sistema de polling

---

## Sistema de sincronización

- Se utiliza polling para obtener la posición del otro jugador.
- La frecuencia de actualización permite una experiencia jugable en tiempo real.
- El movimiento se replica en ambos clientes.

---

## Mecánica del juego

- Los jugadores recolectan comida (bolitas) distribuida en el mapa.
- Cada comida aumenta el puntaje del jugador y hace que crezca su cola.
- Al finalizar el tiempo, se comparan los puntajes.
- Se muestra el ganador en pantalla.

---

## Flujo del juego

1. Pantalla inicial // Nombre de juego e instrucciones + selección de jugador
2. Inicio de partida
3. Recolección de comida y movimiento
4. Final de partida por tiempo
5. Pantalla de resultados
6. Opción de reinicio o salida

---

## UI / UX

El juego incluye:
- Menú de inicio
- Visualización de puntajes
- Pantalla de resultados
- Botones de interacción (jugar, reiniciar, salir)

---

## Entorno

- Escenario 3D simple
- Jugadores diferenciados por color
- Comida (bolitas) de diferentes colores con esferitas simples
- Elementos visuales claros para la interacción
- Post-Processing basico con aberracion Cromatica y glow para que los elementos y los jugadores brillen.
  Se agrego la aberracion para que haya cierta dificultad en el juego para cada jugador en ver las bolitas y que se confundan con el entorno.

---

## Cómo ejecutar

1. Abrir el proyecto en Unity
2. Ejecutar la escena principal
3. Seleccionar jugador (Azul o Rojo)
4. Ejecutar dos instancias:
   - Editor + Build
   - O dos builds (Recomendable)

---

## Requisitos técnicos

- Unity 6.x
- Conexión al servidor proporcionado (Docker)

---

## Limitaciones conocidas

- La comida solo esta sincronizada al momento de entrar a partida, cuando un jugador come una bolita sale otra automaticamente en el mapa entonces el sistema que randomiza el prefab de la comida se vuelve unico para cada jugador ya que no está sincronizada entre clientes (cada cliente genera sus propios objetos), pero esto no es notorio gracias al tamaño del mapa y la cantidad de bolitas que existen en el entorno.
- El resultado del juego se calcula localmente.
- Puede haber ligeros desfases debido al polling pero muy leves.
- En el editor puede aparecer un warning de cámara al reiniciar (no afecta la build).

### Advertencia en Unity Editor: "No Cameras Rendering"

Durante la ejecución del juego en el Editor de Unity puede aparecer el mensaje **"No Cameras Rendering"** al reiniciar la escena.  

Este comportamiento ocurre debido a la lógica de activación de cámaras en el proyecto. Cada jugador posee una cámara, pero únicamente la cámara del jugador local se activa dinámamente en tiempo de ejecución. Durante el proceso de reinicio o cambio de escena, puede existir un breve instante en el que ninguna cámara se encuentra activa, lo que provoca que Unity muestre dicha advertencia.

Es importante destacar que:

- Este mensaje **no representa un error crítico**.
- **No afecta la jugabilidad ni la lógica del sistema multijugador**.
- Se puede seguir eligiendo e interactuando con el menu de elección de jugador a pesar del mensaje y sigue su funcionamiento igual.
- En la versión compilada (build), el flujo de escenas se ejecuta de manera más estable, por lo que el problema no es perceptible para el usuario final.

Este comportamiento es esperado dado el manejo dinámico de cámaras en un entorno multijugador local.

### limitación sobre el puntaje

Debido a la naturaleza del sistema multijugador basado en polling y a las limitaciones del servidor, pueden presentarse pequeñas variaciones en el puntaje entre ambos jugadores.

Estas diferencias pueden ocurrir porque:
- Los jugadores pueden iniciar la partida en momentos ligeramente distintos.
- La generación de comida no está sincronizada entre clientes, por lo que cada jugador ve instancias diferentes.
- En algunos casos, una comida puede generarse muy cerca o directamente sobre un jugador, siendo recolectada de forma inmediata en un cliente pero no en el otro.

Estas variaciones son esperadas y no afectan el funcionamiento general del juego ya que el jugador que tiene mas bolitas es el que termina ganando.

---

## Autores

- Alejandra Acevedo y Natalia Martin

---

## Conclusión

Este proyecto demuestra la implementación de un sistema multijugador básico utilizando un servidor central (Docker) mediante polling HTTP, cumpliendo con las restricciones de no modificar el servidor, en este caso el Script ServerData.cs.
