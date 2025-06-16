
import logo from './logo.svg';
import './App.css';
import React, { useEffect, useState } from "react";
import * as signalR from "@microsoft/signalr";
import Button from '@mui/material/Button';
import Card from '@mui/material/Card';
import CardContent from '@mui/material/CardContent';
import CircularProgress from '@mui/material/CircularProgress';
import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import Divider from '@mui/material/Divider';
import Chip from '@mui/material/Chip';
import ListItemText from '@mui/material/ListItemText';


const createOrder = async () => {

  const response = await fetch("http://localhost:5000/api/order", {
    method: "POST",
    headers: {
      "Content-Type": "application/json"
    },
    body: JSON.stringify({
      customerId: "customer_789",
      status: "Pending",
      created: "2025-06-10T15:30:00Z",
      totalValue: 260,
      items: [
        { productId: "product_777", value: 120, quantity: 2 },
        { productId: "product_888", value: 45, quantity: 3 },
        { productId: "product_999", value: 95, quantity: 1 }
      ],
      payment: {
        PaymentMethod: "CreditCard", PaymentStatus: "Pending"
      }
    })
  })
    .then(response => response.json())
    .then(data => console.log(data))
    .catch(error => console.error("Error:", error));
};



function App() {
  const [orderId, setOrderId] = useState("");
  const [messages, setMessages] = useState([]);
  const [isLoading, setIsLoading] = useState(false);
  useEffect(() => {
    const connection = new signalR.HubConnectionBuilder()
      .withUrl("http://localhost:5001/notifications")
      .configureLogging(signalR.LogLevel.Information)
      .build();

    connection.on("ReceiveMessage", (msg) => {
      setIsLoading(false)
      const order = JSON.parse(msg);
      if (orderId == "") {
        setOrderId(order.Id);
      }

      setMessages((prevMessages) => [...prevMessages, {
        Status: order.Status,
        Updated: new Date().toLocaleString()
      }]);

    });

    connection.start()
      .then(() => console.log("SignalR Connected"))
      .catch(err => console.error("Connection error:", err));

    return () => connection.stop();
  }, []);

  const handleClick = () => {
    setIsLoading(true);
    createOrder()
  };

  return (
    <div className="App">
      <header className="App-header">
        <Card>
          <CardContent>
            <Button variant="contained" onClick={handleClick}>Create Order</Button>
            <div style={{ marginTop: "5px" }}>
              {isLoading && <CircularProgress />}
            </div>
            {orderId != "" ?

              <List >
                <Divider />
                <h6>Tracking order: {orderId}</h6>
                <Divider />
                {messages.map((msg, index) => (
                  <ListItem key={index} sx={{
                    justifyContent: "space-between",

                    // display: "flex",
                    // flexGrow: 1,
                    // minWidth: "200px"
                  }}>
                    <Divider component="li" />
                    <ListItemText primary={msg.Updated} />
                    
                    <Chip label={msg.Status} color="primary" variant="outlined" style={{ marginLeft: "5px" }} />
                  </ListItem>
                ))}
              </List>
              : ""}
          </CardContent>
        </Card>
      </header>
    </div>
  )
}

export default App;

