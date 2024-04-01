using Microsoft.Extensions.Options;
using NLog;
using ScalesApi.Contracts;
using System.IO.Ports;

namespace ScalesApi.Services;

public class SerialPortService : ISerialPortService
{
    private readonly SerialPortServiceConfiguration _configuration;
    private readonly SerialPort _serialPort;
    private readonly Logger _logger = LogManager.GetCurrentClassLogger();

    public SerialPortService(IOptions<SerialPortServiceConfiguration> configuration) {
        _configuration = configuration.Value;

        // Инициализация порта
        _serialPort = new SerialPort();

        // Имя порта
        _serialPort.PortName = _configuration.PortName ?? _serialPort.PortName;

        // Скорость передачи
        _serialPort.BaudRate = _configuration.BaudRate > 0 
            ? _configuration.BaudRate 
            : _serialPort.BaudRate;

        // Бит контроля четности
        _serialPort.Parity = _configuration.Parity ?? _serialPort.Parity;

        // Количество бит данных в кадре передачи данных
        _serialPort.DataBits = _configuration.DataBits > 0 
            ? _configuration.DataBits 
            : _serialPort.DataBits;

        // Количество стоп-битов
        _serialPort.StopBits = _configuration.StopBits ?? _serialPort.StopBits;

        // Тип контроля передачи
        _serialPort.Handshake = _configuration.Handshake ?? _serialPort.Handshake;

        // Срок ожидания в миллисекундах для завершения операции чтения
        _serialPort.ReadTimeout = _configuration.ReadTimeout > 0 
            ? _configuration.ReadTimeout 
            : _serialPort.ReadTimeout;

        // Срок ожидания в миллисекундах для завершения операции записи
        _serialPort.WriteTimeout = _configuration.WriteTimeout > 0 
            ? _configuration.WriteTimeout 
            : _serialPort.WriteTimeout;
    }

    public string ReadLine()
    {
        try
        {
            _serialPort.Open();

            // Считывание неполноценной строки
            string line = _serialPort.ReadLine().Trim();
            _logger.Debug($"Read defective \"{line}\" from \"{_serialPort.PortName}\"");

            // Считывание полноценной строки
            line = _serialPort.ReadLine().Trim();
            _logger.Debug($"Read \"{line}\" from \"{_serialPort.PortName}\"");

            return line;
        }
        finally { _serialPort.Close(); }
    }
}
