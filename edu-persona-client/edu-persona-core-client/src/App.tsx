import "./App.css";
import { AuthInitializer } from "./components";
import AppRoutes from "./routes/AppRoutes";

function App() {
  return (
    <AuthInitializer>
      <AppRoutes />
    </AuthInitializer>
  );
}

export default App;
