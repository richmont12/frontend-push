import {
    HubConnection,
    HubConnectionBuilder,
    LogLevel,
  } from "@microsoft/signalr";
  
  export class SignalR {
    public static instance: HubConnection;
    constructor() {}
  
    static Connection(): Promise<void> {
      const connection = new HubConnectionBuilder()
        .withUrl("https://localhost:60001/notiHub")
        .configureLogging(LogLevel.Information)
        .build();
  
      this.instance = connection;
  
      return connection.start();
    }
  }