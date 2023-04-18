# API для весов

RESTful API[^rest-footnote] для получения данных с весов через COM-порт.

[^rest-footnote]: REST (REpresentational State Transfer) - это подход к проектированию веб-сервисов, который использует протокол HTTP для передачи данных между клиентом и сервером. RESTful системы характеризуются тем, что они не хранят информацию о состоянии клиента на сервере между запросами.

## Маршруты

### __GET__ `/api/scales/weight`

#### Описание:
Получает данные с весов для объекта с уникальным идентификатором `id`.

#### Параметры:
- `id` (необязательный): Уникальный идентификатор объекта.

#### Ответ:
Возвращает JSON-объект со следующими свойствами:
- `weight` (число): Значение измерения веса.
- `unit` (строка): Единица измерения веса.
- `timestamp` (строка): Дата и время измерения веса.

## Конфигурация

Настройки хранятся в файле в файле [appsettings.json](appsettings.json).

### Serial Port

Секция `"SerialPort"` содержит настройки для 
конфигурации COM-порта при обмене данными с внешним устройством.

- `"PortName"` задает имя COM-порта.

- `"BaudRate"` задает скорость передачи данных в битах в секунду.
Значение _9600_ часто используется для последовательной передачи данных.

- `"Parity"` задает бит контроля четности, используемый для проверки ошибок.
Значение _0_ означает, что контроль четности не используется.

- `"DataBits"` задает количество бит данных в каждом кадре передачи данных.
Обычно используется значение _8_.

- `"StopBits"` задает количество стоп-битов в каждом кадре передачи данных.
Обычно используется значение _1_.

- `"Handshake"` задает тип контроля передачи данных.
Значение _0_ означает, что контроль передачи не используется.

- `"WriteTimeout"` и `"ReadTimeout"` задают время ожидания завершения операций
записи и чтения в миллисекундах.