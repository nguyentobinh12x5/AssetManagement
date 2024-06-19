import { APP_CONFIG } from "../../constants/appConfig";
import ConfirmDisable from "../manage-user/list/ConfirmDisable";

const Home = () => {
    return (
        <div className="container m-auto p-5">
            <h1>Welcome to Asset Management {APP_CONFIG.ENVIRONMENT}</h1>
            <p>Test Dev ENV</p>
        </div>
    )
}

export default Home;


