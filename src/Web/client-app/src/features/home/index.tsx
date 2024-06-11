import { APP_CONFIG } from "../../constants/appConfig";

const Home = () => {
    return (
        <div className="container m-auto p-5">
            <h1>Welcome to Asset Management {APP_CONFIG.ENVIRONMENT}</h1>
        </div>
    )
}

export default Home;