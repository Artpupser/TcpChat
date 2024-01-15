using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcpLib.Network;
namespace TcpServer.Network;

public class ServerRequests
{
    [TcpRequest("SignUp")] //Этап 1
    public static Task SignUp(TcpObject tcpObj)
    {
        /*
        Здесь будет регистрация
        Пример формы:
        Логин
        Почта
        Отдача:
        Введите код
        */
        return Task.CompletedTask;
    }
    [TcpRequest("SignIn")] //Этап 1
    public static Task SignIn(TcpObject tcpObj)
    {
        /*
        Здесь будет вход
        Пример формы:
        Логин
        Почта
        Отдача:
        Введите код
        */
        return Task.CompletedTask;
    }
    [TcpRequest("Verify")] // Этап переходник
    public static Task Verify(TcpObject tcpObj)
    {
        /*
        Здесь будет подтверждение почты
        Пример формы:
        Код
        Отдача:
        Код верный или не верный
        */
        return Task.CompletedTask;
    }
}