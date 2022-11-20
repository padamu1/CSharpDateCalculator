- Setter

```
public void SetTargetDate(DateTime targetDate);
public void SetTargetDayOfWeek(DayOfWeek targetDayOfWeek);
public void SetTargetHour(int targetHour);
public void SetTargetMinute(int targetMinute);
```

- Getter

```
public int GetDay();
public int GetHour();
public int GetMinute();
public double GetTotalMilliseconds();
```

-----

- 지정된 분 / 시 / 요일 / 날짜 까지 남은 일수 및 요일 등 계산


```
Calculator calculator = new Calculator();
calculator.SetTargetHour(13);
calculator.SetTargetDayOfWeek(DayOfWeek.Sunday);
calculator.Calculate();
Console.WriteLine("남은 날짜 : {0}   남은 시간 : {1}", calculator.GetDay(),calculator.GetHour());
```



![image](https://user-images.githubusercontent.com/26586104/202908554-51f1a934-c1c0-4283-9646-38b752e6b424.png)
