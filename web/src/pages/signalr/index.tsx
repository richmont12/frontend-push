import { Layout } from "@/components/layout/layout";
import React, { useEffect, useState } from "react";
import {
    HttpTransportType,
    HubConnection,
    HubConnectionBuilder,
    LogLevel,
} from "@microsoft/signalr";


export default function SignalR() {
    const [mighty, setMighty] = useState([]);
    const [conn, setConnection] = React.useState<HubConnection>(null);

    React.useEffect(() => {
        async function start() {
            try {
                const connection = new HubConnectionBuilder()
                    .withUrl("https://localhost:6001/notiHub", {
                        transport: HttpTransportType.ServerSentEvents,
                    })
                    .configureLogging(LogLevel.Information)
                    .build();
                setConnection(connection);

                connection.on("CurrentValue", (currentTime) => {
                    console.log("got current value" + currentTime);
                    setMighty(currentTime);
                });

                await connection.start();
                await connection.invoke("GetValue");
                console.log("SignalR connection established.");
            } catch (error) {
                console.log(error);
                console.log("SignalR error connecting.");
            }
        }

        start();
    }, []);
    return (
        <Layout>

            Signalr
            <br />
            <p>{mighty}</p>

        </Layout>
    )
}
