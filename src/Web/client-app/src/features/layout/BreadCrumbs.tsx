import React from "react";
import { useAppState } from "../../redux/redux-hooks";
import { Link, useLocation } from "react-router-dom";
import { ChevronRight } from "react-bootstrap-icons";

interface ICrumbOption {
  label: string;
  path: string;
  active: boolean;
}

const crumbsMap = new Map<string, string>([
  ["/", "Home"],
  ["/user", "Manage User"],
  ["/user/edit/:id", "Edit User"],
  ["/user/create", "Create User"],
  ["/assets", "Manage Asset"],
  ["/assets/create", "Create New Asset"],
  ["/assignments", "Manage Assignment"],
  ["/assignments/create", "Create New Assignment"],
  ["/assets/edit/:id", "Edit Asset"],
  ["/assignments", "Manage Assignment"],
  ["/assignments/create", "Create New Assignment"],
  ["/assignments/edit/:id", "Edit Assignment"],
  ["/returnings", "Request for Returning"],
]);

const getCrumbLabel = (path: string) => {
  for (let [key, value] of crumbsMap.entries()) {
    if (key.includes(":id")) {
      const pattern = key.replace(":id", ".*");
      const regex = new RegExp(`^${pattern}$`);
      if (regex.test(path)) {
        return value;
      }
    } else if (key === path) {
      return value;
    }
  }
  return null; // Fallback label
};

const BreadCrumbs = () => {
  const location = useLocation();
  const pathnames = location.pathname.split("/").filter((x) => x);
  const { isAuthenticated } = useAppState((state) => state.auth);

  if (!isAuthenticated)
    return (
      <div className="d-flex align-items-center gap-2">
        <img
          alt="Online asset management icon"
          src="/images/Logo_lk.png"
          className="header-logo"
        />
        <p className="headText">Online Asset Management</p>
      </div>
    );

  if (!pathnames.length)
    return (
      <div className="d-flex align-items-center gap-2">
        <BreadCrumbItem path={"/"} label={crumbsMap.get("/") ?? ""} />
      </div>
    );
  return (
    <div className="d-flex align-items-center gap-2">
      {pathnames.map((value, index) => {
        const to = `/${pathnames.slice(0, index + 1).join("/")}`;
        const isLast = pathnames.length === index + 1;
        const label = getCrumbLabel(to);

        if (!label) return null;

        return (
          <React.Fragment key={to}>
            <BreadCrumbItem key={to} path={to} label={label} />
            {!isLast && <ChevronRight color="#fff" />}
          </React.Fragment>
        );
      })}
    </div>
  );
};

export default BreadCrumbs;

interface IBreadCrumbItemProps {
  path: string;
  label: string;
}
const BreadCrumbItem: React.FC<IBreadCrumbItemProps> = ({ path, label }) => {
  return (
    <Link to={path} className="bread-crumbs-item">
      {label}
    </Link>
  );
};
