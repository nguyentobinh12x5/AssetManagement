import React from "react";
import Header from "./Header";
import Sidebar from "./Sidebar";

interface Props {
  children: React.ReactNode;
  showSidebar: boolean;
}

const Layout: React.FC<Props> = ({ children, showSidebar }) => {
  return (
    <>
      <Header />
      <div className="container-lg-min container-fluid">
        <div className="row mt-5">
          {showSidebar && (
            <div className="col-lg-2 col-md-4 col-12 mr-5">
              <Sidebar />
            </div>
          )}

          <div className={`${showSidebar ? "col-lg-9 col-md-7" : "col-12"}`}>
            {children}
          </div>
        </div>
      </div>
    </>
  );
};

export default Layout;
