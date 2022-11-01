import { Counter } from "./components/Counter";
import { FetchData } from "./components/FetchData";
import { Home } from "./components/Home";
import { ListUser } from "./views/users/ListUser.js";
import Login from "./views/users/Login";

const AppRoutes = [
  {
    index: true,
    element: <Home />
  },
  {
    path: '/counter',
    element: <Counter />
  },
  {
    path: '/fetch-data',
    element: <FetchData />
  },
  {
    path: '/users',
    element: <ListUser />
  },
  {
    path: '/users/login',
    element: <Login />
  },
];

export default AppRoutes;
