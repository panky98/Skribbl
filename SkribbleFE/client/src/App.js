import { BrowserRouter as Router, Route, Switch } from 'react-router-dom';
import logo from './logo.svg';
import './App.css';
import Home from "./Components/Home"
import NavBar from "./Components/NavBar"
import Error from "./Components/Error"
import Soba from "./Components/Soba"


function App() {
  return (
    <Router>
    <NavBar/>
    <Switch>
      <Route exact path="/">
        <Home/>
      </Route>
      <Route exact path="/Soba">
        <Soba/>
      </Route>
      <Route  path="*">
        <Error />
      </Route>
    </Switch>
</Router>
  );
}

export default App;
