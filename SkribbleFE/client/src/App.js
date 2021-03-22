import { BrowserRouter as Router, Route, Switch } from 'react-router-dom';
import logo from './logo.svg';
import './App.css';
import Home from "./Components/Home"
import NavBar from "./Components/NavBar"
import Error from "./Components/Error"
import Sobe from "./Components/Sobe"
import Soba from "./Components/Soba"
import LogIn from './Components/LogIn.js';
import SignUp from './Components/SignUp.js';
import Kategorije from './Components/Kategorije';
import Leaderboard from "./Components/Leaderboard"
import Logout from "./Components/Logout";
import MyReplays from "./Components/MyReplays";

function App() {
  return (
    <Router>
    <NavBar/>
    <Switch>
      <Route exact path="/">
        <Home/>
      </Route>
      <Route exact path="/Sobe">
        <Sobe/>
      </Route>
      <Route exact path="/Soba/:sobaId" component={Soba}></Route>
      <Route exact path="/LogIn" component={LogIn}></Route>
      <Route exact path="/SignUp" component={SignUp}></Route>
      <Route exact path="/Kategorije" component={Kategorije}></Route>
      <Route exact path="/Leaderboard" component={Leaderboard}></Route>
      <Route exact path="/Logout" component={Logout}></Route>
      <Route exact path="/MyReplays" component={MyReplays}></Route>
      <Route  path="*">
        <Error />
      </Route>
    </Switch>
</Router>
  );
}

export default App;
