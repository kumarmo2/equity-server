import { useEffect, useState } from "react";

function App() {
  const [positions, setPositions] = useState([]);

  useEffect(() => {
    const fn = async () => {
      const response = await fetch("/api/trades");
      if (!response) {
        return;
      }
      const result = await response.json();
      console.log(result);
      setPositions(result);
    };
    fn();
  }, []);
  return (
    <>
      <div>
        <h1>Positions</h1>

        {positions.map((position) => {
          return (
            <li key={position.id}>
              <span>{position.securityCode}</span>:{" "}
              <span>{position.quantity}</span>
            </li>
          );
        })}
      </div>
    </>
  );
}

export default App;
